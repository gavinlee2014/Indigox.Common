using System;
using System.Collections.Generic;

namespace Indigox.Common.NHibernate.Extension.Utils
{
    internal class TypeName
    {
        private static TypeName instance = new TypeName();

        public static TypeName Instance
        {
            get { return instance; }
        }

        public TypeName()
        {
            typeToAlias = new Dictionary<Type, string>();
            aliasToType = new Dictionary<string, Type>();
            RegisterDefaultTypes();
        }

        public Type GetType( string typeName )
        {
            if ( IsAliasDefined( typeName ) )
            {
                return aliasToType[ typeName ];
            }
            else
            {
                return Type.GetType( typeName );
            }
        }

        public string GetTypeName( Type type )
        {
            if ( IsTypeDefined( type ) )
            {
                return typeToAlias[ type ];
            }
            else
            {
                return type.AssemblyQualifiedName;
            }
        }

        private bool IsAliasDefined( string alias )
        {
            return aliasToType.ContainsKey( alias );
        }

        private bool IsTypeDefined( Type type )
        {
            return typeToAlias.ContainsKey( type );
        }

        /// <summary>
        /// init
        /// </summary>
        private void RegisterDefaultTypes()
        {
            RegisterTypeAlias( typeof( string ), "string" );
            RegisterTypeAlias( typeof( bool ), "bool", "boolean" );
            RegisterTypeAlias( typeof( char ), "char" );
            RegisterTypeAlias( typeof( byte ), "byte" );
            RegisterTypeAlias( typeof( int ), "int" );
            RegisterTypeAlias( typeof( long ), "long" );
            RegisterTypeAlias( typeof( short ), "short" );
            RegisterTypeAlias( typeof( uint ), "uint" );
            RegisterTypeAlias( typeof( ulong ), "ulong" );
            RegisterTypeAlias( typeof( ushort ), "ushort" );
            RegisterTypeAlias( typeof( float ), "float" );
            RegisterTypeAlias( typeof( double ), "double" );
            RegisterTypeAlias( typeof( decimal ), "decimal" );
            RegisterTypeAlias( typeof( DateTime ), "datetime", "date" );
            RegisterTypeAlias( typeof( Guid ), "guid" );
        }

        private void RegisterTypeAlias( Type type, params string[] alias )
        {
            foreach ( string a in alias )
            {
                aliasToType.Add( a, type );
            }
            typeToAlias.Add( type, alias[ 0 ] );
        }

        private Dictionary<Type, string> typeToAlias;
        private Dictionary<string, Type> aliasToType;
    }
}