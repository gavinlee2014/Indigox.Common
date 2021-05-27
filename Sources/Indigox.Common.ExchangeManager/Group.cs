using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Indigox.Common.ExchangeManager
{
    public class Group : BaseEntity
    {
        public Group()
            : base(new Hashtable())
        {
        }

        public Group(Hashtable properties)
            : base(properties)
        {
        }

        public string OrganizationalUnit
        {
            get { return Convert.ToString(this.properties["OrganizationalUnit"]); }
        }

        public string OrganizationalUnitDN
        {
            get { return this.DistinguishedName.Substring(this.DistinguishedName.IndexOf("OU")); }
        }
    }
}
