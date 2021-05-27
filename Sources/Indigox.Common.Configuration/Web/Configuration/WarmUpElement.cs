using System;
using System.Configuration;

namespace Indigox.Common.Configuration.Web.Configuration
{
    public class WarmUpElement : ConfigurationElement
    {
        [ConfigurationProperty( "name" )]
        public string Name
        {
            get
            {
                return (string)base[ "name" ];
            }
            set
            {
                base[ "name" ] = value;
            }
        }

        [ConfigurationProperty( "type" )]
        public string TypeName
        {
            get
            {
                return (string)base[ "type" ];
            }
            set
            {
                base[ "type" ] = value;
            }
        }
    }
}