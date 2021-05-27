using System;
using System.Collections.Generic;

namespace Indigox.Common.Configuration
{
    internal class TypeAlias
    {
        static TypeAlias()
        {
            Register( "int", typeof( int ) );
            Register( "short", typeof( short ) );
            Register( "long", typeof( long ) );
            Register( "uint", typeof( uint ) );
            Register( "ushort", typeof( ushort ) );
            Register( "ulong", typeof( ulong ) );
            Register( "float", typeof( float ) );
            Register( "double", typeof( double ) );
            Register( "decimal", typeof( decimal ) );
            Register( "bool", typeof( bool ) );
            Register( "datetime", typeof( DateTime ) );
            Register( "byte", typeof( byte ) );
            Register( "bytes", typeof( byte[] ) );
            Register( "guid", typeof( Guid ) );
            Register( "char", typeof( char ) );
            Register( "string", typeof( string ) );
        }

        private static Dictionary<string, Type> aliasToTypeMap = new Dictionary<string, Type>();
        private static Dictionary<Type, string> typeToAliasMap = new Dictionary<Type, string>();

        private static void Register( string alias, Type type )
        {
            aliasToTypeMap.Add( alias, type );
            if ( !typeToAliasMap.ContainsKey( type ) )
            {
                typeToAliasMap.Add( type, alias );
            }
        }

        private static void Register( string alias, Type type, bool asDefaultAlias )
        {
            aliasToTypeMap.Add( alias, type );
            if ( typeToAliasMap.ContainsKey( type ) )
            {
                if ( asDefaultAlias )
                    typeToAliasMap[ type ] = alias;
            }
            else
            {
                typeToAliasMap.Add( type, alias );
            }
        }

        public static bool Contains( string alias )
        {
            return aliasToTypeMap.ContainsKey( alias );
        }

        public static Type GetType( string alias )
        {
            return aliasToTypeMap[ alias ];
        }

        public static bool HasAlias( Type type )
        {
            return typeToAliasMap.ContainsKey( type );
        }

        public static string GetAlias( Type type )
        {
            return typeToAliasMap[ type ];
        }
    }
}