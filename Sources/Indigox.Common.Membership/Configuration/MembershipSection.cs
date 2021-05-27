using System;
using System.Configuration;

namespace Indigox.Common.Membership.Configuration
{
    public class MembershipSection : ConfigurationSection
    {
        private static readonly string DEFAULT_SECTION_NAME = "indigo/membership";

        private static MembershipSection _Default;

        public static MembershipSection Default
        {
            get
            {
                if ( _Default == null )
                {
                    _Default = ( (MembershipSection)ConfigurationManager.GetSection( DEFAULT_SECTION_NAME ) );
                }
                return _Default;
            }
        }

        public static MembershipSection Get( string name )
        {
            return ( (MembershipSection)ConfigurationManager.GetSection( name ) );
        }

        [ConfigurationProperty( "providerFactory" )]
        public ProviderFactorySetting ProviderFactory
        {
            get { return (ProviderFactorySetting)base[ "providerFactory" ]; }
            set { base[ "providerFactory" ] = value; }
        }
    }
}