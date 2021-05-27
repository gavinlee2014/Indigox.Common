using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.Net;
using Indigox.Common.Logging;

namespace Indigox.Common.ADAccessor.ObjectModel.AD
{
    class PropertyCreater
    {
        private static Dictionary<string, List<string>> propertyMap;

        static PropertyCreater()
        {
            propertyMap = new Dictionary<string, List<string>>()
            {
                //{"ID", new List<string>(){"objectGUID"}},
                {"Name", new List<string>(){"name", "cn"}},
                {"DisplayName", new List<string>(){"displayName"}},
                {"Account", new List<string>(){"sAMAccountName","userPrincipalName"}},
                {"Phone", new List<string>(){"telephoneNumber"}},
                {"Mobile", new List<string>(){"mobile"}},
                {"Mail", new List<string>(){"mail"}},
                {"Department", new List<string>(){"department"}},
                {"Company", new List<string>(){"company"}},
                {"Title", new List<string>(){"title"}},
                {"Fax", new List<string>() {"facsimileTelephoneNumber"}},
                {"FamilyName", new List<string>() {"sn"}},
                {"GivenName", new List<string>() {"givenName"}},
                {"Description", new List<string>() {"description"}},
                {"Portrait", new List<string>() {"thumbnailPhoto"}},                
            };
        }

        public static void SetProperty(DirectoryEntry dn, string propertyName, string propertyValue)
        {
            if (!propertyMap.ContainsKey(propertyName))
            {
                return;
            }

            if ((dn.SchemaEntry.Name.Equals("organizationalUnit")) && (propertyName.Equals("Name")))
            {
                return;
            }

            List<string> adPropertyNames = propertyMap[propertyName];
            foreach (string adPropertyName in adPropertyNames)
            {
                if ((!dn.SchemaEntry.Name.Equals("user")) && (adPropertyName.Equals("userPrincipalName")))
                {
                    continue;
                }

                string adPropertyValue = propertyValue;

                if ((adPropertyName.Equals("thumbnailPhoto")))
                {
                    if (!String.IsNullOrEmpty(adPropertyValue))
                    {
                        if (!adPropertyValue.StartsWith("http:")) adPropertyValue = "http:" + adPropertyValue;
                        Log.Debug("头像URL: " + adPropertyValue);
                        try
                        {
                            ProfileService profileService = new ProfileService(adPropertyValue);
                            byte[] buffer = profileService.GetThumbnailImageForAD();
                            dn.Properties["thumbnailPhoto"].Add(buffer);
                        }
                        catch
                        {
                            Log.Debug("取头像失败。头像链接: " + adPropertyValue);
                        }
                    }
                    continue;
                }

                if ((!String.IsNullOrEmpty(adPropertyValue)) && (adPropertyName.Equals("userPrincipalName")))
                {
                    adPropertyValue = adPropertyValue + "@" + Accessor.GetDomainName();
                }

                if (dn.Properties.Contains(adPropertyName))
                {
                    if (string.IsNullOrEmpty(adPropertyValue))
                    {
                        object o = dn.Properties[adPropertyName].Value;
                        dn.Properties[adPropertyName].Remove(o);
                    }
                    else
                    {
                        dn.Properties[adPropertyName][0] = adPropertyValue;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(adPropertyValue))
                    {
                        dn.Properties[adPropertyName].Add(adPropertyValue);
                    }
                }
            }
        }
    }
}
