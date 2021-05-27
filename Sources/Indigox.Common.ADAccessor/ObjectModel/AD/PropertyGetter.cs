using System.Collections.Generic;
using System.DirectoryServices;
using System;
using System.Text;

namespace Indigox.Common.ADAccessor.ObjectModel.AD
{
    class PropertyGetter
    {
        private static Dictionary<string, string> propertyMap;

        static PropertyGetter()
        {
            propertyMap = new Dictionary<string, string>()
            {
                {"ID", "objectGUID"},
                {"Name", "name"},
                {"DisplayName", "displayName"},
                {"Account", "sAMAccountName"},
                {"Phone", "telephoneNumber"},
                {"Mobile", "mobile"},
                {"Mail", "mail"}
            };
        }

        public static object GetProperty(DirectoryEntry de, string propertyName)
        {
            if (propertyName.Equals("Parent"))
            {
                return de.Parent.Guid;
            }

            string adAttrName = propertyName;
            if (propertyMap.ContainsKey(propertyName))
            {
                adAttrName = propertyMap[propertyName];
            }

            if (de.Properties.Contains(adAttrName))
            {
                if (adAttrName.Equals("objectGUID"))
                {
                    
                    //byte[] bytes = (byte[])de.Properties[adAttrName][0];
                    //return new Guid(GetByteString(bytes));
                    return de.Guid;
                }
                return de.Properties[adAttrName][0];
            }
            else
            {
                return null;
            }
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
    }
}
