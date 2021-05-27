using System;
using System.Configuration;

namespace Indigox.Common.Data.Configuration
{
    public class DatabaseSection : ConfigurationSection
    {
        private const string STR_Connections = "connections";
        private const string STR_Properties = "properties";

        [ConfigurationProperty( STR_Connections )]
        [ConfigurationCollection( typeof( ConnectionElement ) )]
        public ConnectionElementCollection Connections
        {
            get
            {
                return (ConnectionElementCollection)base[ STR_Connections ];
            }
            set
            {
                base[ STR_Connections ] = value;
            }
        }

        private static DatabaseSection _Default;

        public static DatabaseSection Default
        {
            get
            {
                if ( _Default == null )
                {
                    _Default = ( (DatabaseSection)ConfigurationManager.GetSection( "indigo/database" ) );
                }
                return _Default;
            }
        }

        public static DatabaseSection Get( string name )
        {
            return ( (DatabaseSection)ConfigurationManager.GetSection( name ) );
        }
    }
}