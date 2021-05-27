using System;

namespace Indigox.Common.Configuration.Test
{
    internal class ClassHelper
    {
        public static bool IsGenericTypeOf( Type type, Type genericTypeDefine )
        {
            return ( type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefine );
        }

        public static Type MakeGenericType( Type genericTypeDefine, params Type[] types )
        {
            try
            {
                Type nullableType = genericTypeDefine.MakeGenericType( types );
                return nullableType;
            }
            catch ( Exception ex )
            {
                string[] argTypeNames = new string[ types.Length ];
                for ( int i = 0; i < types.Length; i++ )
                {
                    argTypeNames[ i ] = GetTypeName( types[ i ] );
                }
                throw new Exception( "can't make generic type '" + GetTypeName( genericTypeDefine ) + "' with argument types '"
                    + string.Join( ", ", argTypeNames ) + "'.", ex );
            }
        }

        public static Type GetGenericArgumentType( Type type, int index )
        {
            if ( !type.IsGenericType )
            {
                throw new ArgumentException( "type '" + GetTypeName( type ) + "' is not an generic type." );
            }
            return type.GetGenericArguments()[ index ];
        }

        public static string GetTypeName( Type type )
        {
            if ( type.IsGenericType )
            {
                Type[] argTypes = type.GetGenericArguments();
                string[] argTypeNames = new string[ argTypes.Length ];
                for ( int i = 0; i < argTypes.Length; i++ )
                {
                    argTypeNames[ i ] = GetTypeName( argTypes[ i ] );
                }
                return string.Format( "{0}<{1}>", type.Name.Substring( 0, type.Name.IndexOf( '`' ) ), string.Join( ", ", argTypeNames ) );
            }
            return type.Name;
        }
    }
}
