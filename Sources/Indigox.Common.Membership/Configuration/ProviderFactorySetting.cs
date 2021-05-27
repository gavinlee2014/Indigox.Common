using System;
using System.Configuration;

namespace Indigox.Common.Membership.Configuration
{
    public class ProviderFactorySetting : ConfigurationElement
    {
        [ConfigurationProperty( "type" )]
        public string ProviderFactoryType
        {
            get { return (string)base[ "type" ]; }
            set { base[ "type" ] = value; }
        }
    }
}