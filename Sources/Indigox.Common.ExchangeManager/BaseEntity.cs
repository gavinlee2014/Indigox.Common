using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Indigox.Common.ExchangeManager
{
    public abstract class BaseEntity
    {
        protected Hashtable properties = new Hashtable();

        public BaseEntity(Hashtable properties)
        {
            this.properties = properties;
        }

        public Guid ID
        {
            get { return (Guid)this.properties["Guid"]; }
        }

        public string Identity
        {
            get { return Convert.ToString(this.properties["Identity"]); }
            set { this.properties["Identity"] = value; }
        }

        public string Name
        {
            get { return Convert.ToString(this.properties["Name"]); }
            set { this.properties["Name"] = value; }
        }

        public string DisplayName
        {
            get { return Convert.ToString(this.properties["DisplayName"]); }
            set { this.properties["DisplayName"] = value; }
        }

        public string DistinguishedName
        {
            get { return Convert.ToString(this.properties["DistinguishedName"]); }
            set { this.properties["DistinguishedName"] = value; }
        }
    }
}
