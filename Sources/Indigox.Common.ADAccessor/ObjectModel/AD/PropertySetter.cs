using System.Collections.Generic;
using System.DirectoryServices;
using System;
using System.Net;
using System.IO;
using Indigox.Common.Logging;

namespace Indigox.Common.ADAccessor.ObjectModel.AD
{
    class PropertySetter
    {
        private static Dictionary<string, List<string>> propertyMap;

        static PropertySetter()
        {
            propertyMap = new Dictionary<string, List<string>>()
            {
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

        public static void SetProperty(ref DirectoryEntry dn, string propertyName, string propertyValue)
        {
            Log.Debug("set property: " + propertyName +",   value: "+propertyValue+ ",  db shchema entry name: " + dn.SchemaEntry.Name);

            if (!propertyMap.ContainsKey(propertyName))
            {
                return;
            }

            if (dn.SchemaEntry.Name.Equals("organizationalUnit") && propertyName.Equals("Name"))
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

                if ((!String.IsNullOrEmpty(adPropertyValue)) && (adPropertyName.Equals("userPrincipalName")))
                {
                    adPropertyValue = adPropertyValue + "@" + Accessor.GetDomainName();
                }

                if (adPropertyName.Equals("thumbnailPhoto"))
                {
                    updatePortrait(dn, adPropertyValue);
                    continue;
                }

                if (adPropertyName.Equals("mail"))
                {
                    updateMail(dn, adPropertyValue);
                    continue;
                }

                DoSetProperty(dn, adPropertyName, adPropertyValue);
            }
        }

        private static void updateMail(DirectoryEntry dn, string adPropertyValue)
        {
            string MailPrimaryPrefix = "SMTP";

            PropertyValueCollection proxyAddresses = dn.Properties["proxyAddresses"];
            if (string.IsNullOrEmpty(adPropertyValue))
            {
                removeCurrentPrimaryMailAddressAndNewMailIfExist(adPropertyValue, proxyAddresses);
                if (proxyAddresses.Count > 0)
                {
                    movePrimeSmtpToNext(MailPrimaryPrefix, proxyAddresses);
                }
                
            }
            else
            {
                if (proxyAddresses != null)
                {
                    removeCurrentPrimaryMailAddressAndNewMailIfExist(adPropertyValue, proxyAddresses);
                    proxyAddresses.Add(MailPrimaryPrefix + ":" + adPropertyValue);
                }
            }
            
            string nickName = String.IsNullOrEmpty(adPropertyValue) ? "" : adPropertyValue.Substring(0, adPropertyValue.IndexOf("@"));
            DoSetProperty(dn, "mailNickName", nickName);
            DoSetProperty(dn, "mail", adPropertyValue);
        }

        private static void movePrimeSmtpToNext(string MailPrimaryPrefix, PropertyValueCollection proxyAddresses)
        {
            string newPrimeSmtp = proxyAddresses[0].ToString();
            proxyAddresses.Remove(newPrimeSmtp);
            newPrimeSmtp = MailPrimaryPrefix + ":" + newPrimeSmtp.Substring(newPrimeSmtp.IndexOf(":"));
            proxyAddresses.Add(newPrimeSmtp);
        }

        private static void removeCurrentPrimaryMailAddressAndNewMailIfExist(string mail, PropertyValueCollection proxyAddresses)
        {
            List<string> indexesToRemove = new List<string>();
            string MailPrimaryPrefix = "SMTP";
            //string MailSecondaryPrefix = "smtp";
            //string MailPagerPrefix = "smtp-pager";
            for (int i = 0; i < proxyAddresses.Count; i++)
            {
                string proxyAddress = proxyAddresses[i].ToString();
                if (proxyAddress.StartsWith(MailPrimaryPrefix + ":"))
                {
                    indexesToRemove.Add(proxyAddress);
                }
                if (proxyAddress.EndsWith(":" + mail))
                {
                    indexesToRemove.Add(proxyAddress);
                }
            }
            foreach (string index in indexesToRemove)
            {
                proxyAddresses.Remove(index);
            }
        }

        private static void DoSetProperty(DirectoryEntry dn, string adPropertyName, string adPropertyValue)
        {
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

        private static void updatePortrait(DirectoryEntry dn, string adPropertyValue)
        {
            if (String.IsNullOrEmpty(adPropertyValue))
            {
                dn.Properties["thumbnailPhoto"].Clear();
            }
            else
            {
                if (!adPropertyValue.StartsWith("http:")) adPropertyValue = "http:" + adPropertyValue;
                Log.Debug("头像URL: " + adPropertyValue);
                try
                {
                    ProfileService profileService = new ProfileService(adPropertyValue);
                    byte[] buffer = profileService.GetThumbnailImageForAD();

                    dn.Properties["thumbnailPhoto"].Clear();
                    dn.Properties["thumbnailPhoto"].Add(buffer);
                }
                catch
                {
                    Log.Debug("取头像失败。头像链接: " + adPropertyValue);
                }
            }
        }
    }
}
