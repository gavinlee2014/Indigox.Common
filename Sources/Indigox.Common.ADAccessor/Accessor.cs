using System;
using System.Configuration;
using System.DirectoryServices;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Indigox.Common.ADAccessor.ObjectModel;
using Indigox.Common.ADAccessor.ObjectModel.AD;
using Indigox.Common.Logging;

namespace Indigox.Common.ADAccessor
{
    enum PasswordStrategy
    {
        DEFAULT,
        MOBILE,
        IDCARD,
    }
    public class Accessor
    {
        private const string STR_ADServer = "ADServer";
        private const string STR_ADRootName = "ADRootName";
        private const string STR_ADRootGroupName = "ADRootGroupName";
        private const string STR_ADLeaveName = "ADLeaveName";
        private const string STR_ADUsername = "ADUsername";
        private const string STR_ADPassword = "ADPassword";
        private const string STR_ADDefaultUserPassword = "ADDefaultUserPassword";
        private const string STR_ADDefaultUserPasswordPrefix = "ADDefaultUserPasswordPrefix";
        private const string STR_ADPasswordStrategy = "ADPasswordStrategy";

        /// <summary>
        /// ADServer 服务器名称，为空时自动选择连接到当前域的域控服务器，其它例如： 
        ///     "ad01"
        ///     "ad01.contoso.local"
        ///     "192.168.10.1"
        /// </summary>
        private static string ADServer;
        /// <summary>
        /// AD 的根节点的 distinguishedName，为空时自动选择当前域的根节点，其它例如：
        ///     "DC=contoso,DC=local"
        ///     "OU=集团总公司,DC=contoso,DC=local"
        /// </summary>
        private static string ADRootDN;

        /// <summary>
        /// 用于存放离职人员的OU
        /// </summary>
        private static string ADLeaveDN;

        private static string ADRootGroup;
        /// <summary>
        /// 连接到 ADServer 时使用的用户名
        /// </summary>
        private static string ADUsername;
        /// <summary>
        /// 连接到 ADServer 时使用的用户密码
        /// </summary>
        private static string ADPassword;

        private static string ADDefaultUserPassword;

        private static string ADDefaultUserPasswordPrefix;

        private static PasswordStrategy ADPasswordStrategy = PasswordStrategy.DEFAULT;

        private static ADServerStatus status = ADServerStatus.Unknow;
        private static DateTime lastTryTime = DateTime.MinValue;
        private static readonly int minTryInterval = 5;

        static Accessor()
        {
            ADServer = ConfigurationManager.AppSettings[STR_ADServer];
            ADRootDN = ConfigurationManager.AppSettings[STR_ADRootName];
            ADLeaveDN = ConfigurationManager.AppSettings[STR_ADLeaveName];
            ADRootGroup = ConfigurationManager.AppSettings[STR_ADRootGroupName];
            ADUsername = ConfigurationManager.AppSettings[STR_ADUsername];
            ADPassword = ConfigurationManager.AppSettings[STR_ADPassword];
            ADDefaultUserPassword = ConfigurationManager.AppSettings[STR_ADDefaultUserPassword];
            ADDefaultUserPasswordPrefix = ConfigurationManager.AppSettings[STR_ADDefaultUserPasswordPrefix];
            if (String.IsNullOrEmpty(ADDefaultUserPasswordPrefix))
            {
                ADDefaultUserPasswordPrefix = "Indigox@";
            }
            string pwdStrategy = ConfigurationManager.AppSettings[STR_ADPasswordStrategy];
            switch (pwdStrategy)
            {
                case "DEFAULT":
                    ADPasswordStrategy = PasswordStrategy.DEFAULT;
                    break;
                case "MOBILE":
                    ADPasswordStrategy = PasswordStrategy.MOBILE;
                    break;
                case "IDCARD":
                    ADPasswordStrategy = PasswordStrategy.IDCARD;
                    break;
                default:
                    ADPasswordStrategy = PasswordStrategy.DEFAULT;
                    break;
            }
        }


        private static bool CheckADServerStatus()
        {
            switch (status)
            {
                case ADServerStatus.Unknow:
                    if (TryConnectServer() == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case ADServerStatus.Connecting:
                    while (status == ADServerStatus.Connecting)
                    {
                        Thread.Sleep(100);
                        switch (status)
                        {
                            case ADServerStatus.Connecting:
                                return true;
                            case ADServerStatus.Error:
                                return false;
                        }
                    }
                    break;

                case ADServerStatus.Connected:
                    break;

                case ADServerStatus.Error:
                    if ((DateTime.Now - lastTryTime).TotalSeconds <= minTryInterval)
                    {
                        return false;
                    }
                    else
                    {
                        if (TryConnectServer() == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
            }
            // default return true
            return true;
        }

        private static string BuildPath(string server, string dn)
        {
            bool isServerNull = string.IsNullOrEmpty(server);
            bool isDNNull = string.IsNullOrEmpty(dn);
            string path = "";
            if (!isServerNull && !isDNNull)
            {
                path = string.Format("LDAP://{0}/{1}", server, dn);
            }
            else if (!isServerNull)
            {
                path = string.Format("LDAP://{0}", server);
            }
            else if (!isDNNull)
            {
                path = string.Format("LDAP://{0}", dn);
            }
            return path;
        }

        private static void ConnectServer()
        {
            if (!CheckADServerStatus())
            {
                throw new ApplicationException("AD Server connect fail");
            }
        }

        private static int TryConnectServer()
        {
            return TryConnectServer(ADServer);
        }

        private static int TryConnectServer(string ADController)
        {
            DirectoryEntry de = null;

            string path = BuildPath(ADController, "");

            de = GetDirectoryEntry(path);

            status = ADServerStatus.Connecting;
            lastTryTime = DateTime.Now;
            try
            {
                de.RefreshCache();
                //string name = de.Name;
                status = ADServerStatus.Connected;
                return 0;
            }
            catch (COMException ex)
            {
                status = ADServerStatus.Error;
                return ex.ErrorCode;
            }
        }

        private static string GetGuidByteString(Guid id)
        {
            return GetByteString(id.ToByteArray());
        }

        private static string GetByteString(byte[] bs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.AppendFormat("\\{0}", b.ToString("X").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        private static DirectoryEntry GetDirectoryEntry()
        {
            return new DirectoryEntry();
        }

        private static DirectoryEntry GetDirectoryEntry(string path)
        {
            if ((!String.IsNullOrEmpty(ADUsername)) && (!String.IsNullOrEmpty(ADPassword)))
            {
                return new DirectoryEntry(path, ADUsername, ADPassword);
            }
            else
            {
                return new DirectoryEntry(path);
            }
        }

        private static void CreateAllProperties(Entry entry, DirectoryEntry newde)
        {
            PropertyInfo[] properties = entry.GetType().GetProperties();
            foreach (PropertyInfo item in properties)
            {
                //if (item.Name.Equals("ID"))
                //{
                //    continue;
                //}
                PropertyCreater.SetProperty(newde, item.Name, Convert.ToString(item.GetValue(entry, null)));
            }
            //entry.Name
            try
            {
                newde.CommitChanges();
            }
            catch (DirectoryServicesCOMException e)
            {
                throw new Exception(e.Message + ",DirectoryEntry name=" + entry.Name, e);
            }
        }

        private static void UpdateAllProperties(Entry entry, DirectoryEntry newde)
        {
            PropertyInfo[] properties = entry.GetType().GetProperties();
            foreach (PropertyInfo item in properties)
            {
                PropertySetter.SetProperty(ref newde, item.Name, Convert.ToString(item.GetValue(entry, null)));
            }
            //entry.Name
            try
            {
                newde.CommitChanges();

                if (entry.Name != newde.Name)
                {
                    if (newde.SchemaEntry.Name.Equals("organizationalUnit"))
                    {
                        newde.Rename("OU=" + entry.Name);
                    }
                    else
                    {
                        newde.Rename("CN=" + entry.Name);
                    }
                }
            }
            catch (DirectoryServicesCOMException e)
            {
                throw new Exception(e.Message + ",DirectoryEntry name=" + entry.Name, e);
            }
        }

        private static void GatherAllProperties(Entry entry, DirectoryEntry de)
        {
            if (de == null)
            {
                return;
            }
            PropertyInfo[] properties = entry.GetType().GetProperties();
            foreach (PropertyInfo item in properties)
            {
                object value = PropertyGetter.GetProperty(de, item.Name);
                item.SetValue(entry, value, null);
            }
        }

        private static void Delete(DirectoryEntry de)
        {
            de.DeleteTree();
            de.Dispose();
            de = null;
        }

        private static bool Contains(DirectoryEntry container, DirectoryEntry de)
        {
            bool _isExistUser = false;
            if (container.Properties["member"].IndexOf(de.Properties["distinguishedName"].Value) > -1)
            {
                _isExistUser = true;
            }
            else
            {
                _isExistUser = false;
            }

            return _isExistUser;
        }


        private static SearchResultCollection Find(string filter)
        {
            ConnectServer();

            DirectorySearcher ds = new DirectorySearcher();
            DirectoryEntry root = GetSysRoot();
            ds.SearchRoot = root;
            SearchResultCollection results = null;

            try
            {
                // AD限制了每次结果最多返回1000条记录,
                // 所以要取出所有记录必须将PageSize设置成小于1000的数,
                // PageSize的默认值是0表示不分页
                ds.PageSize = 500;

                ds.Filter = filter;
                results = ds.FindAll();
            }
            finally
            {
                ds.Dispose();
            }

            return results;
        }

        private static SearchResultCollection Find(DirectoryEntry container, string filter)
        {
            ConnectServer();

            DirectorySearcher ds = new DirectorySearcher(container);
            SearchResultCollection results = null;

            try
            {
                // AD限制了每次结果最多返回1000条记录,
                // 所以要取出所有记录必须将PageSize设置成小于1000的数,
                // PageSize的默认值是0表示不分页
                ds.PageSize = 500;

                ds.Filter = filter;
                results = ds.FindAll();
            }
            finally
            {
                ds.Dispose();
            }

            return results;
        }

        private static SearchResult FindOne(string filter)
        {
            ConnectServer();

            DirectorySearcher ds = new DirectorySearcher();
            DirectoryEntry root = GetSysRoot();
            ds.SearchRoot = root;
            SearchResult results = null;

            try
            {
                // AD限制了每次结果最多返回1000条记录,
                // 所以要取出所有记录必须将PageSize设置成小于1000的数,
                // PageSize的默认值是0表示不分页
                ds.PageSize = 500;

                ds.Filter = filter;
                results = ds.FindOne();
            }
            finally
            {
                ds.Dispose();
            }

            return results;
        }

        private static SearchResult FindOne(DirectoryEntry container, string filter)
        {
            ConnectServer();

            DirectorySearcher ds = new DirectorySearcher(container);
            SearchResult results = null;

            try
            {
                // AD限制了每次结果最多返回1000条记录,
                // 所以要取出所有记录必须将PageSize设置成小于1000的数,
                // PageSize的默认值是0表示不分页
                ds.PageSize = 500;

                ds.Filter = filter;
                results = ds.FindOne();
            }
            finally
            {
                ds.Dispose();
            }

            return results;
        }

        private static DirectoryEntry GetSysRoot()
        {
            return GetDirectoryEntry(BuildPath(ADServer, ""));
        }

        private static DirectoryEntry GetRoot()
        {
            ConnectServer();
            DirectoryEntry root = null;
            DirectoryEntry sysRoot = GetSysRoot();

            try
            {
                root = sysRoot.Children.Find("OU=" + ADRootDN);
            }
            catch
            {
                root = sysRoot.Children.Add("OU=" + ADRootDN, "organizationalUnit");
                root.CommitChanges();
            }
            return root;
        }


        public static string GetLeaveOU()
        {

            /***
             * 修改GetLeaveOU，原逻辑为直接查找Root的OU，现修改为根据DN查找
             * 修改人：曾勇
             * 修改时间：2018-11-12
             **/

            ConnectServer();
            DirectoryEntry leaveOU = null;
            try
            {
                leaveOU = GetDirectoryEntry(BuildPath(ADServer, ADLeaveDN));
                return leaveOU.Guid.ToString();
            }
            catch (Exception)
            {
                Log.Error("Can't find LeaveOU by DN :" + ADLeaveDN);
            }

            return null;
            /***            
            DirectoryEntry sysRoot = GetSysRoot();

            try
            {
                leaveOU = sysRoot.Children.Find("OU=" + ADLeaveDN);
            }
            catch
            {
                leaveOU = sysRoot.Children.Add("OU=" + ADLeaveDN, "organizationalUnit");
                leaveOU.CommitChanges();
            }            
            return leaveOU.Guid.ToString();
            **/
        }

        private static DirectoryEntry GetRootGroup()
        {
            ConnectServer();
            DirectoryEntry root = GetRoot();
            DirectoryEntry rootGroupDE = null;
            try
            {
                rootGroupDE = root.Children.Find("CN=" + ADRootGroup, "group");
            }
            catch
            {
            }
            if (rootGroupDE == null)
            {
                rootGroupDE = root.Children.Add("CN=" + ADRootGroup, "group");
                Group rootGroup = new Group();
                rootGroup.Name = ADRootGroup;
                rootGroup.Mail = ADRootGroup + "@";
                UpdateAllProperties(rootGroup, rootGroupDE);
                rootGroupDE.CommitChanges();
            }
            return rootGroupDE;
        }

        /// <summary>
        /// 根据 distinguishedName 获取 AD 对象
        /// </summary>
        /// <param name="dn">
        /// distinguishedName，为空时自动选择当前域的根节点，其它例如：
        ///     "DC=contoso,DC=local"
        ///     "OU=集团总公司,DC=contoso,DC=local"
        /// </param>
        /// <returns></returns>
        private static DirectoryEntry GetByDN(string dn)
        {
            if (string.IsNullOrEmpty(dn))
            {
                return GetDirectoryEntry();
            }

            ConnectServer();

            SearchResult result = FindOne("(distinguishedName=" + dn + ")");

            DirectoryEntry de = null;

            if (result != null)
            {
                de = result.GetDirectoryEntry();
            }

            return de;
        }

        /// <summary>
        /// 根据 GUID 获取 AD 对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static DirectoryEntry GetByGuid(Guid id)
        {
            ConnectServer();

            SearchResult result = FindOne("(objectGUID=" + GetGuidByteString(id) + ")");

            if (result == null)
            {
                return null;
            }

            return GetDirectoryEntry(result.GetDirectoryEntry().Path);
        }

        /// <summary>
        /// 根据 GUID 获取 AD 对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static DirectoryEntry GetByAccountName(string accountName)
        {
            if (accountName == null || accountName.Trim() == "")
            {
                throw new ArgumentNullException("accountName");
            }

            ConnectServer();

            SearchResult result = FindOne("(sAMAccountName=" + accountName + ")");

            if (result == null)
            {
                return null;
            }
            //DirectoryEntry r = GetDirectoryEntry(result.GetDirectoryEntry().Path);
            //Console.WriteLine(r.SchemaEntry.Name);
            return GetDirectoryEntry(result.GetDirectoryEntry().Path);
        }

        private static DirectoryEntry GetByName(string containerID, string name)
        {
            if (name == null || name.Trim() == "")
            {
                throw new ArgumentNullException("name");
            }

            ConnectServer();

            DirectoryEntry container = null;
            if (String.IsNullOrEmpty(containerID))
            {
                container = GetRoot();
            }
            else
            {
                container = GetByGuid(new Guid(containerID));
            }

            SearchResult result = FindOne(container, "(name=" + name + ")");

            DirectoryEntry de = null;

            if (result != null)
            {
                de = result.GetDirectoryEntry();
            }

            return de;
        }

        private static bool ChildOUExist(DirectoryEntry container, string objectName)
        {
            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = container;

            deSearch.Filter = "(&(objectClass=OrganizationalUnit) (OU=" + objectName + "))";
            SearchResultCollection results = deSearch.FindAll();
            if (results.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static DirectoryEntry GetChildOU(DirectoryEntry container, string objectName)
        {
            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = container;

            deSearch.Filter = "(&(objectClass=OrganizationalUnit) (OU=" + objectName + "))";
            SearchResult results = deSearch.FindOne();
            return results.GetDirectoryEntry();
        }

        private static void SetUserOrganization(ref DirectoryEntry de)
        {
            string companyName = null;
            string departmentName = null;
            if (de.Parent != null)
            {
                string leaveOUGuid = GetLeaveOU();
                if (!de.Parent.Guid.ToString().Equals(leaveOUGuid))
                {
                    departmentName = (string)PropertyGetter.GetProperty(de.Parent, "Name");
                    while (departmentName.IndexOf(".") > 0)
                    {
                        departmentName = departmentName.Substring(departmentName.IndexOf(".") + 1);
                    }

                    if (de.Parent.Parent != null)
                    {
                        companyName = (string)PropertyGetter.GetProperty(de.Parent.Parent, "Name");
                        while (companyName.IndexOf(".") > 0)
                        {
                            companyName = companyName.Substring(companyName.IndexOf(".") + 1);
                        }
                    }
                }
                else
                {
                    companyName = (string)de.Properties["Company"].Value;
                    departmentName = (string)de.Properties["Department"].Value;
                }
            }
            Log.Debug("Department:" + departmentName);
            Log.Debug("Company:" + companyName);
            PropertySetter.SetProperty(ref de, "Company", companyName);
            PropertySetter.SetProperty(ref de, "Department", departmentName);

        }

        public static string GetDomainName()
        {
            DirectoryEntry root = GetSysRoot();
            string dn = root.Properties["distinguishedName"].Value as String;
            string domainName = dn.Replace("DC=", "").Replace(",", ".");
            return domainName;
        }

        public static User GetUserByAccount(string accountName)
        {
            DirectoryEntry de = GetByAccountName(accountName);
            if (de == null)
            {
                return null;
            }
            User user = new User();
            GatherAllProperties(user, de);
            return user;
        }

        public static Group GetGroupByAccount(string accountName)
        {
            DirectoryEntry de = GetByAccountName(accountName);
            if (de == null)
            {
                return null;
            }
            Group group = new Group();
            GatherAllProperties(group, de);
            return group;
        }

        public static Group GetGroupByID(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new NullReferenceException("Group id cannot be null");
            }
            DirectoryEntry de = GetByGuid(new Guid(id));
            if (de == null)
            {
                return null;
            }
            Group group = new Group();
            GatherAllProperties(group, de);
            return group;
        }

        public static Group GetDefaultGroup()
        {
            DirectoryEntry de = GetRootGroup();
            Group group = new Group();
            GatherAllProperties(group, de);
            return group;
        }

        public static OrganizationalUnit GetOrganizationByByID(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new NullReferenceException("Organization id cannot be null");
            }
            DirectoryEntry de = GetByGuid(new Guid(id));
            if (de == null)
            {
                return null;
            }
            OrganizationalUnit ou = new OrganizationalUnit();
            GatherAllProperties(ou, de);
            return ou;
        }

        public static OrganizationalUnit GetDefaultOrganization()
        {
            DirectoryEntry de = GetRoot();
            OrganizationalUnit ou = new OrganizationalUnit();
            GatherAllProperties(ou, de);
            return ou;
        }

        public static User CreateUser(string containerID, User user)
        {
            Log.Debug(String.Format("CreateUser {0},{1},{2} begin", user.Name, user.DisplayName, user.Account));
            ConnectServer();

            DirectoryEntry container = null;
            if (String.IsNullOrEmpty(containerID))
            {
                container = GetRoot();
            }
            else
            {
                container = GetByGuid(new Guid(containerID));
            }

            DirectoryEntry newde = container.Children.Add("CN=" + user.Name, "user");
            SetUserOrganization(ref newde);
            CreateAllProperties(user, newde);
            string pwd;
            switch (ADPasswordStrategy)
            {
                case PasswordStrategy.DEFAULT:
                    pwd = ADDefaultUserPassword;
                    break;
                case PasswordStrategy.MOBILE:
                    if (!String.IsNullOrEmpty(user.Mobile))
                    {
                        pwd = ADDefaultUserPasswordPrefix + user.Mobile;
                    }
                    else
                    {
                        pwd = ADDefaultUserPassword;
                    }
                    break;
                case PasswordStrategy.IDCARD:
                    if ((!String.IsNullOrEmpty(user.IdCard)) && (user.IdCard.Length >= 6))
                    {
                        pwd = ADDefaultUserPasswordPrefix + user.IdCard.Substring(user.IdCard.Length - 6, 6);
                    }
                    else
                    {
                        pwd = ADDefaultUserPassword;
                    }
                    break;
                default:
                    pwd = ADDefaultUserPassword;
                    break;
            }

            newde.Invoke("SetPassword", new Object[] { pwd });

            GatherAllProperties(user, newde);
            //AddToGroup(user, org);
            Log.Debug(String.Format("CreateUser {0},{1},{2},{3},{4},{5} end", user.Name, user.DisplayName, user.Account, user.IdCard, pwd, ADPasswordStrategy + ""));
            return user;
        }

        public static OrganizationalUnit CreateOrganization(string containerID, OrganizationalUnit ou)
        {
            Log.Debug(String.Format("CreateOrganization {0},{1} begin", ou.Name, ou.DisplayName));
            ConnectServer();

            DirectoryEntry container = null;
            if (String.IsNullOrEmpty(containerID))
            {
                container = GetRoot();
            }
            else
            {
                container = GetByGuid(new Guid(containerID));
            }

            DirectoryEntry newde = null;
            if (ChildOUExist(container, ou.Name))
            {
                newde = GetChildOU(container, ou.Name);
            }
            else
            {
                newde = container.Children.Add("OU=" + ou.Name, "organizationalUnit");
                newde.CommitChanges();
            }
            //UpdateAllProperties(ou, newde);
            GatherAllProperties(ou, newde);
            Log.Debug(String.Format("CreateOrganization {0},{1} end", ou.Name, ou.DisplayName));
            return ou;
        }

        public static Group CreateGroup(string containerID, Group group)
        {
            Log.Debug(String.Format("CreateGroup {0},{1},{2} begin", group.Name, group.DisplayName, group.Account));
            ConnectServer();

            DirectoryEntry container = null;
            if (String.IsNullOrEmpty(containerID))
            {
                container = GetRoot();
            }
            else
            {
                container = GetByGuid(new Guid(containerID));
            }

            ADGroupTypeEnmu typeNum = ADGroupTypeEnmu.ADS_GROUP_TYPE_UNIVERSAL_GROUP | ADGroupTypeEnmu.ADS_GROUP_TYPE_SECURITY_ENABLED;
            //int typeNum = (int)(ADGroupTypeEnmu.ADS_GROUP_TYPE_UNIVERSAL_GROUP | ADGroupTypeEnmu.ADS_GROUP_TYPE_SECURITY_ENABLED);

            DirectoryEntry newde = container.Children.Add("CN=" + group.Name, "group");
            newde.Properties["groupType"].Add((int)typeNum);
            CreateAllProperties(group, newde);
            GatherAllProperties(group, newde);

            Log.Debug(String.Format("CreateGroup {0},{1},{2} end", group.Name, group.DisplayName, group.Account));
            //AddToGroup(group, org);
            return group;
        }

        public static void UpdateUser(User user)
        {
            Log.Debug(String.Format("UpdateUser {0},{1},{2} begin", user.Name, user.DisplayName, user.Account));
            DirectoryEntry de = GetByGuid(user.ID);
            SetUserOrganization(ref de);
            UpdateAllProperties(user, de);
            Log.Debug(String.Format("UpdateUser {0},{1},{2} end", user.Name, user.DisplayName, user.Account));
        }

        public static void UpdateGroup(Group group)
        {
            Log.Debug(String.Format("UpdateGroup {0},{1},{2} begin", group.Name, group.DisplayName, group.Account));
            DirectoryEntry groupDe = GetByGuid(group.ID);
            UpdateAllProperties(group, groupDe);
            /*
             * 修改时间：2018-09-06
             * 修改人：曾勇
             * 修改内容：修改群组信息时，更新群组成员的公司和部门信息
             */

            foreach (string childDn in groupDe.Properties["member"])
            {
                DirectoryEntry child = GetByDN(childDn);
                if (child.SchemaClassName == "user")
                {
                    SetUserOrganization(ref child);
                    child.CommitChanges();
                }
            }


            Log.Debug(String.Format("UpdateGroup {0},{1},{2} end", group.Name, group.DisplayName, group.Account));
        }

        public static void UpdateOrganizationalUnit(OrganizationalUnit ou)
        {
            Log.Debug(String.Format("UpdateOrganizationalUnit {0},{1} begin", ou.Name, ou.DisplayName));
            DirectoryEntry de = GetByGuid(ou.ID);
            UpdateAllProperties(ou, de);
            Log.Debug(String.Format("UpdateOrganizationalUnit {0},{1} end", ou.Name, ou.DisplayName));
        }

        public static void DeleteUser(string userID)
        {
            DirectoryEntry de = GetByGuid(new Guid(userID));
            Delete(de);
        }

        public static void DeleteGroup(string groupID)
        {
            DirectoryEntry de = GetByGuid(new Guid(groupID));
            Delete(de);
        }

        public static void DeleteOrganizationalUnit(string ouID)
        {
            DirectoryEntry de = GetByGuid(new Guid(ouID));
            SearchResultCollection users = Find(de, "(objectClass=user)");
            if (users.Count > 0)
            {
                throw new Exception("OU删除失败，OU下还有User，OU：" + de.Name);
            }

            Delete(de);
        }

        public static void MoveTo(string entryID, string containerID)
        {
            Log.Debug(String.Format("MoveTo {0},{1}", entryID, containerID));
            DirectoryEntry de = GetByGuid(new Guid(entryID));
            DirectoryEntry container = null;
            if (String.IsNullOrEmpty(containerID))
            {
                container = GetRoot();
            }
            else
            {
                container = GetByGuid(new Guid(containerID));
            }
            string dn1 = (string)de.Properties["distinguishedName"].Value;
            string dn2 = (string)container.Properties["distinguishedName"].Value;
            string dn3 = (string)de.Parent.Properties["distinguishedName"].Value;
            if (dn2 == dn3)
            {
                if (de.SchemaEntry.Name.Equals("user"))
                {
                    SetUserOrganization(ref de);
                }
                de.CommitChanges();
                return;
            }

            if (dn1 != de.Path)
            {
                DirectoryEntry de2 = GetByDN(dn1);
                if (dn2 != container.Path)
                {
                    DirectoryEntry container2 = GetByDN(dn2);
                    de2.MoveTo(container2);
                }
                else
                {
                    de2.MoveTo(container);
                }
                if (de2.SchemaEntry.Name.Equals("user"))
                {
                    SetUserOrganization(ref de2);
                }
                de2.CommitChanges();

                de = de2;
            }
            else
            {
                if (dn2 != container.Path)
                {
                    DirectoryEntry container2 = GetByDN(dn2);
                    de.MoveTo(container2);
                }
                else
                {
                    de.MoveTo(container);
                }
                if (de.SchemaEntry.Name.Equals("user"))
                {
                    SetUserOrganization(ref de);
                }
                de.CommitChanges();
            }
        }

        public static void AddToGroup(string entryID, string groupID)
        {
            Log.Debug(String.Format("AddToGroup {0},{1}", entryID, groupID));
            if (String.IsNullOrEmpty(groupID))
            {
                groupID = Convert.ToString(PropertyGetter.GetProperty(GetRootGroup(), "ID"));
            }

            DirectoryEntry de = GetByGuid(new Guid(entryID));
            DirectoryEntry container = null;
            if (String.IsNullOrEmpty(groupID))
            {
                container = GetRootGroup();
            }
            else
            {
                container = GetByGuid(new Guid(groupID));
            }

            if (Contains(container, de))
            {
                return;
            }
            container.Properties["member"].Add(de.Properties["distinguishedName"].Value);
            try
            {
                container.CommitChanges();
            }
            catch (DirectoryServicesCOMException e)
            {
                throw new Exception(e.Message
                    + ",DirectoryEntry container.Name=" + container.Name + ", de.Name=" + de.Name, e);
            }
            if (container != null) container.Dispose();
            if (de != null) de.Dispose();
        }

        public static void RemoveFromGroup(string entryID, string groupID)
        {
            Log.Debug(String.Format("RemoveFromGroup {0},{1}", entryID, groupID));
            DirectoryEntry de = GetByGuid(new Guid(entryID));
            Log.Debug("Get AD group : " + entryID + " as " + de.Name);
            DirectoryEntry container = null;
            if (String.IsNullOrEmpty(groupID))
            {
                container = GetRootGroup();
            }
            else
            {
                container = GetByGuid(new Guid(groupID));
            }
            Log.Debug("Get AD group : " + groupID + " as " + container.Name);

            if (!Contains(container, de))
            {
                return;
            }
            Log.Debug("remove " + de.Name + " from " + container.Name + " begin...");
            container.Properties["member"].Remove(de.Properties["distinguishedName"].Value);
            //container.Invoke("remove", new object[] { "LDAP://" + ADServer + "/" + de.Properties["distinguishedName"].Value });
            container.CommitChanges();
            Log.Debug("remove " + de.Name + " from " + container.Name + " end.");
            if (container != null) container.Dispose();
            if (de != null) de.Dispose();
        }

        public static void UnlockUser(string accountName)
        {
            DirectoryEntry de = GetByAccountName(accountName);
            de.InvokeSet("IsAccountLocked", false);
            de.CommitChanges();
            if (de != null) de.Dispose();
        }

        public static void DisableUser(string userID)
        {
            DirectoryEntry de = GetByGuid(new Guid(userID));
            int val = (int)de.Properties["userAccountControl"].Value;
            de.Properties["userAccountControl"].Value = val | ADConst.AccountDisable;
            de.CommitChanges();
            if (de != null) de.Dispose();
        }

        public static void EnableUser(string userID)
        {
            DirectoryEntry de = GetByGuid(new Guid(userID));
            int val = (int)de.Properties["userAccountControl"].Value;
            de.Properties["userAccountControl"].Value = ADConst.NormalAccount | ADConst.DontExpirePassword;
            de.CommitChanges();
            if (de != null) de.Dispose();
        }

        public static bool IsUserExist(string accountName)
        {
            return GetByAccountName(accountName) != null;
        }

        public static bool IsUserEnabled(string accountName)
        {
            DirectoryEntry de = GetByAccountName(accountName);
            if (de != null)
            {
                int userAccountControl = Convert.ToInt32(de.Properties["userAccountControl"][0]);
                if (de != null) de.Dispose();

                int accountDisableSign = Convert.ToInt32(ADConst.AccountDisable);
                int flagExists = userAccountControl & accountDisableSign;

                if (flagExists > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckPassword(string accountName, string pwd)
        {
            if (!IsUserEnabled(accountName))
            {
                return false;
            }
            DirectoryEntry de = GetByAccountName(accountName);
            string path = de.Path;
            if (de != null) de.Dispose();

            DirectoryEntry check = new DirectoryEntry(path, accountName, pwd, AuthenticationTypes.Secure);

            try
            {
                string name = check.Name;
                return true;
            }
            catch (COMException)
            {
                return false;
            }
            finally
            {
                if (check != null) check.Dispose();
            }
        }

        public static void SetPassword(string accountName, string pwd)
        {
            DirectoryEntry de = GetByAccountName(accountName);
            de.Invoke("SetPassword", new object[] { pwd });
            if (de != null) de.Dispose();
        }

        public static void ChangePassword(string accountName, string oldPwd, string newPwd)
        {
            DirectoryEntry de = GetByAccountName(accountName);
            de.Invoke("ChangePassword", oldPwd, newPwd);
            if (de != null) de.Dispose();
        }
    }

    public enum ADServerStatus
    {
        /// <summary>
        /// 未知，初始化值
        /// </summary>
        Unknow = 0,
        /// <summary>
        /// 连接中，请等待
        /// </summary>
        Connecting = 101,
        /// <summary>
        /// 已连接
        /// </summary>
        Connected = 201,
        /// <summary>
        /// 连接错误，并且在指定时间内不会重试连接
        /// </summary>
        Error = -1
    }

    internal class ADConst
    {
        /// <summary>
        /// 用户帐号禁用标志
        /// </summary>
        public static int AccountDisable = 0X0002;

        /// <summary>
        /// 普通用户的默认帐号类型
        /// </summary>
        public static int NormalAccount = 0X0200;

        /// <summary>
        /// 密码永不过期标志
        /// </summary>
        public static int DontExpirePassword = 0X10000;

    }

    public enum ADGroupTypeEnmu : uint
    {
        ADS_GROUP_TYPE_GLOBAL_GROUP = 0x00000002,
        ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP = 0x00000004,
        ADS_GROUP_TYPE_LOCAL_GROUP = 0x00000004,
        ADS_GROUP_TYPE_UNIVERSAL_GROUP = 0x00000008,
        ADS_GROUP_TYPE_SECURITY_ENABLED = 0x80000000
    };

}
