using System;
using System.Text;

namespace Indigox.Common.Membership.ActiveDirectoryImpl.ActiveDirectory
{
    internal class ADFilter
    {
        private string filter = "";

        public ADFilter( string filter )
        {
            this.filter = filter;
        }

        public static ADFilter And( params ADFilter[] adFilter )
        {
            StringBuilder builder = new StringBuilder();
            builder.Append( "(&" );
            foreach ( ADFilter f in adFilter )
            {
                builder.Append( "(" );
                builder.Append( f.ToString() );
                builder.Append( ")" );
            }
            builder.Append( ")" );
            return new ADFilter( builder.ToString() );
        }

        public static ADFilter Or( params ADFilter[] adFilter )
        {
            StringBuilder builder = new StringBuilder();
            builder.Append( "(|" );
            foreach ( ADFilter f in adFilter )
            {
                builder.Append( "(" );
                builder.Append( f.ToString() );
                builder.Append( ")" );
            }
            builder.Append( ")" );
            return new ADFilter( builder.ToString() );
        }

        public static ADFilter Not( ADFilter adFilter )
        {
            StringBuilder builder = new StringBuilder();
            builder.Append( "(!(" );
            builder.Append( adFilter.ToString() );
            builder.Append( "))" );

            return new ADFilter( builder.ToString() );
        }

        public override string ToString()
        {
            return this.filter;
        }
    }
}
