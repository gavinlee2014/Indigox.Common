using System;
using System.Configuration;

namespace Indigox.Common.Configuration.Web.Configuration
{
    internal class WarmUpSection : ConfigurationSection
    {
        private static readonly string DEFAULT_SECTION_NAME = "indigo/warmup";

        private static WarmUpSection _Default;

        public static WarmUpSection Default
        {
            get
            {
                if ( _Default == null )
                {
                    _Default = ( (WarmUpSection)ConfigurationManager.GetSection( DEFAULT_SECTION_NAME ) );
                }
                return _Default;
            }
        }

        public static WarmUpSection Get( string name )
        {
            return ( (WarmUpSection)ConfigurationManager.GetSection( name ) );
        }

        [ConfigurationCollection( typeof( WarmUpElementCollection ), AddItemName = "add" )]
        [ConfigurationProperty( "elements" )]
        public WarmUpElementCollection WarmUpElements
        {
            get { return (WarmUpElementCollection)base[ "elements" ]; }
            set { base[ "elements" ] = value; }
        }
    }
}