using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Indigox.Common.Logging;
using System.Collections;

namespace Indigox.Common.ExchangeManager
{
    public class ExchangeManagerService
    {
        public Group GetDistributionGroup(string account)
        {
            Group group = null;
            Log.Debug("GetDistributionGroup:" + account);
            PSExecutor executor = new PSExecutor()
               {
                   Script = string.Format(
                       @"Set-AdServerSettings -ViewEntireForest $true; Get-DistributionGroup -Identity '{0}' -ErrorAction SilentlyContinue",
                       account)
               };
            IList<Hashtable> result = executor.Execute();
            if (result.Count > 0)
            {
                group = new Group(result[0]);
            }

            return group;
        }

        public void EnableDistributionGroup(string account)
        {
            /*
             * 将查询的-Anr参数修改为-Identity
             */
            PSExecutor executor = new PSExecutor()
            {
                Script = string.Format(
                    @"Set-AdServerSettings -ViewEntireForest $true; if (!(Get-DistributionGroup -Identity '{0}' -ErrorAction 'SilentlyContinue')) {{ Enable-DistributionGroup -Identity '{0}'}} ", 
                    account)
            };
            executor.Execute();
        }

        public void DisableDistributionGroup(string account)
        {
            PSExecutor executor = new PSExecutor()
            {
                Script = "Set-AdServerSettings -ViewEntireForest $true; Disable-DistributionGroup -Identity '" + account + "' -Confirm:$False"
            };
            executor.Execute();
        }
        
        public void ConnectMailBox(string account,string mailDatabase)
        {
            Log.Debug(account);
            PSExecutor executor = new PSExecutor()
            {
                Script = String.Format(
                    @"Set-AdServerSettings -ViewEntireForest $true; if (!( Get-Mailbox -Identity '{0}')) {{ Connect-Mailbox -Identity '{0}' -Database '{1}' -Alias '{0}' }} ",
                account,mailDatabase)
            };
            executor.Execute();
        }

        public void EnableMailBox(string account)
        {
            Log.Debug(account);
            PSExecutor executor = new PSExecutor()
            {
                Script = String.Format(
                    @"Set-AdServerSettings -ViewEntireForest $true; if (!( Get-Mailbox -Identity '{0}' -ErrorAction SilentlyContinue )) {{ Set-MailUser -Identity '{0}' -ExternalEmailAddress '{1}' -ErrorAction SilentlyContinue;Enable-Mailbox -Identity '{0}';}} ", 
                account, account + "@temp.com")
            };
            executor.Execute();
        }
        public void EnableMailBox(string account,string databse)
        {
            Log.Debug("EnableMailBox Account:" +account + " Database:" + databse);
            PSExecutor executor = new PSExecutor()
            {
                Script = String.Format(
                    @"Set-AdServerSettings -ViewEntireForest $true; if (!( Get-Mailbox -Identity '{0}' -ErrorAction SilentlyContinue )) {{Set-MailUser -Identity '{0}' -ExternalEmailAddress '{1}' -ErrorAction SilentlyContinue;Enable-Mailbox -Identity '{0}' -Database '{2}';}}; ",
                account, account+"@temp.com", databse)
            };
            executor.Execute();
        }

        public void UpdateMailBox(string account, string mail)
        {
            Log.Debug("UpdateMailBox Account:" + account);
            PSExecutor executor = new PSExecutor()
            {
                Script = String.Format(
                    @"Set-AdServerSettings -ViewEntireForest $true; Set-Mailbox -Identity ""{0}"" -Alias ""{0}"" -EmailAddresses ""SMTP:{1}"" -EmailAddressPolicyEnabled $false; Set-Mailbox -Identity ""{0}"" -EmailAddressPolicyEnabled $true;",
                account, mail)
            };
            executor.Execute();
        }

        public void DisableMailBox(string account)
        {
            PSExecutor executor = new PSExecutor()
            {
                Script = "Set-AdServerSettings -ViewEntireForest $true;  if (( Get-Mailbox -Identity '" + account + "' -ErrorAction SilentlyContinue )) {Disable-Mailbox -Identity '" + account + "' -Confirm:$False;}"
            };
            //executor.Parameters.Add("Identity", account);
            executor.Execute();
        }

        public void HideMailBox(string account)
        {
            PSExecutor executor = new PSExecutor()
            {
                Script = "Set-AdServerSettings -ViewEntireForest $true;  if (( Get-Mailbox -Identity '" + account + "' -ErrorAction SilentlyContinue )) {Set-Mailbox -Identity '" + account + "' -HiddenFromAddressListsEnabled $true}"
            };
            executor.Execute();
        }

        public void ShowMailBox(string account)
        {
            PSExecutor executor = new PSExecutor()
            {
                Script = "Set-AdServerSettings -ViewEntireForest $true;  if (( Get-Mailbox -Identity '" + account + "' -ErrorAction SilentlyContinue )) {Set-Mailbox -Identity '" + account + "' -HiddenFromAddressListsEnabled $false}"
            };
            executor.Execute();
        }

        public void LimitMailBox(string account)
        {
            PSExecutor executor = new PSExecutor()
            {
                Script = "Set-AdServerSettings -ViewEntireForest $true;  if (( Get-Mailbox -Identity '" + account + "' -ErrorAction SilentlyContinue )) {Set-Mailbox -Identity '" + account + "' -MaxSendSize 1kb -MaxReceiveSize 1kb}"
            };
            executor.Execute();
        }

        public void UnlimitMailBox(string account)
        {
            PSExecutor executor = new PSExecutor()
            {
                Script = "Set-AdServerSettings -ViewEntireForest $true;  if (( Get-Mailbox -Identity '" + account + "' -ErrorAction SilentlyContinue )) {Set-Mailbox -Identity '" + account + "' -MaxSendSize Unlimited -MaxReceiveSize Unlimited}"
            };
            executor.Execute();
        }

        public AddressList GetAddressList(string name)
        {
            AddressList addressList = null;
            PSExecutor executor = new PSExecutor()
            {
                Script = string.Format(
                    @"Get-AddressList -Identity '{0}' -ErrorAction SilentlyContinue",
                    name)
            };
            Log.Debug(string.Format(
                    @"Get-AddressList -Identity '{0}' -ErrorAction SilentlyContinue",
                    name));
            IList<Hashtable> result = executor.Execute();
            if (result.Count > 0)
            {
                addressList = new AddressList(result[0]);
            }

            return addressList;
        }
        
        public AddressList CreateAddressList(Group group, AddressList container)
        {
            PSExecutor executor = new PSExecutor();
            if(container != null)
            {
                executor.Script = "new-AddressList -Name '" + group.Name + "' -RecipientContainer '" + group.OrganizationalUnitDN + "' -IncludedRecipients 'MailboxUsers, MailGroups' -Container '" + container.Identity + "' -DisplayName '" + group.DisplayName + "'";
            }
            else
            {
                executor.Script = "new-AddressList -Name '" + group.Name + "' -RecipientContainer '" + group.OrganizationalUnitDN + "' -IncludedRecipients 'MailboxUsers, MailGroups' -DisplayName '" + group.DisplayName + "'";
            }
            executor.Execute();

            AddressList created = GetGroupAddressList(group, container);

            executor.Script = "update-AddressList -Identity '" + created.Identity + "'";
            executor.Execute();

            return created;
        }

        public AddressList CreateAdressListWithFilter(Group group, AddressList container)
        {
            if (group.DisplayName.IndexOf('.') == -1)
            {
                return this.CreateAddressList(group, container);
            }

            string prefix = group.DisplayName.Substring(0, group.DisplayName.IndexOf('.'));
            string createScript = "new-AddressList -Name '" + group.Name + "' -DisplayName '" + group.DisplayName + "' -RecipientFilter {(Alias -like '*') -and (DisplayName -like '" + prefix + "*' -or CustomAttribute2 -like '*," + prefix + ",*') -and (ObjectCategory -like 'user' -or ObjectCategory -like 'group')} -Container '" + container.Identity + "'";
            ComplexPSExecutor executor = new ComplexPSExecutor();
            executor.ExecuteScript(createScript);

            AddressList created = GetGroupAddressList(group, container);

            PSExecutor psExecutor = new PSExecutor();
            psExecutor.Script = "update-AddressList -Identity '" + created.Identity + "'";
            psExecutor.Execute();

            return created;
        }

        public AddressList GetAddressList(string name, AddressList container)
        {
            AddressList addressList = null;
            if (container != null)
            {
                addressList = this.GetAddressList(container.Identity + "\\" + name);
            }
            else
            {
                addressList = this.GetAddressList(name);
            }

            return addressList;
        }

        private AddressList GetGroupAddressList(Group group, AddressList container)
        {
            AddressList created = null;
            if (container != null)
            {
                created = this.GetAddressList(container.Identity + "\\" + group.Name);
            }
            else
            {
                created = this.GetAddressList("\\" + group.Name);
            }
            return created;
        }

        public void UpdateAddressListWithFilter(AddressList addressList)
        {
            if (addressList.DisplayName.IndexOf('.') == -1)
            {
               this.UpdateAddressList(addressList);
               return;
            }
            
            string prefix = addressList.DisplayName.Substring(0, addressList.DisplayName.IndexOf('.'));

            ComplexPSExecutor executor = new ComplexPSExecutor();
            executor.ExecuteScript("set-AddressList -Identity '" + addressList.ID + "' -Name '" + addressList.Name + "' -DisplayName '" + addressList.DisplayName+ "' -RecipientFilter {(Alias -like '*') -and (DisplayName -like '" + prefix + "*' -or CustomAttribute2 -like '*," + prefix + ",*') -and (ObjectCategory -like 'user' -or ObjectCategory -like 'group')}");
            
            PSExecutor psExecutor = new PSExecutor();
            psExecutor.Script = "update-AddressList -Identity '" + addressList.ID + "'";
            psExecutor.Execute();
        }

        public void UpdateAddressList(AddressList addressList)
        {
            PSExecutor executor = new PSExecutor();
            executor.Script = "set-AddressList -Identity '" + addressList.ID + "' -Name '" + addressList.Name + "' -DisplayName '" + addressList.DisplayName + "'";
            executor.Execute();

            executor.Script = "update-AddressList -Identity '" + addressList.ID + "'";
            executor.Execute();
        }

        public void DeleteAddressList(AddressList addressList)
        {
            PSExecutor executor = new PSExecutor();
            executor.Script = "Remove-AddressList -Identity '" + addressList.ID + "' -Recursive -Confirm:$False";
            executor.Execute();
        }

        public void MoveAddressList(AddressList addressList, AddressList container)
        {
            PSExecutor executor = new PSExecutor();
            executor.Script = "move-AddressList -Identity '" + addressList.Identity + "' -Target '" + container.Identity + "' -Confirm:$False";
            executor.Execute();
        }
    }
}
