using System;
using System.Configuration;

namespace Indigox.Common.Data.Configuration
{
    public sealed class ConnectionElement : ConfigurationElement
    {
        private const string STR_Name = "name";
        private const string STR_ConnectionString = "connectionString";
        private const string STR_Provider = "providerName";

        [ConfigurationProperty( STR_Name, IsKey = true, IsRequired = true )]
        public string Name
        {
            get
            {
                return (string)base[ STR_Name ];
            }
            set
            {
                base[ STR_Name ] = value;
            }
        }

        [ConfigurationProperty( STR_ConnectionString, IsRequired = true )]
        public string ConnectionString
        {
            get
            {
                return (string)base[ STR_ConnectionString ];
            }
            set
            {
                base[ STR_ConnectionString ] = value;
            }
        }

        [ConfigurationProperty( STR_Provider, IsRequired = false )]
        public string Provider
        {
            get
            {
                return (string)base[ STR_Provider ];
            }
            set
            {
                base[ STR_Provider ] = value;
            }
        }
    }
}