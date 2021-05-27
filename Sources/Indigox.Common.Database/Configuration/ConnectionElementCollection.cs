using System;
using System.Configuration;

namespace Indigox.Common.Data.Configuration
{
    public sealed class ConnectionElementCollection : ConfigurationElementCollection
    {
        private const string STR_Defalut = "default";

        [ConfigurationProperty( STR_Defalut )]
        public string Default
        {
            get
            {
                return (string)base[ STR_Defalut ];
            }
            set
            {
                base[ STR_Defalut ] = value;
            }
        }

        public ConnectionElement DefaultConnection
        {
            get
            {
                return this[ Default ];
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionElement();
        }

        protected override object GetElementKey( ConfigurationElement element )
        {
            return ( (ConnectionElement)element ).Name;
        }

        public new ConnectionElement this[ string name ]
        {
            get
            {
                return (ConnectionElement)base.BaseGet( name );
            }
        }
    }
}