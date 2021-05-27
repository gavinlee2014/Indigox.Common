using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Indigox.Common.NHibernate.Extension.Reflection
{
    internal class TypeInfo
    {
        public static bool IsCollection( Type type )
        {
            if ( typeof( IList ).IsAssignableFrom( type ) )
            {
                return true;
            }
            return false;
        }

        public static bool IsGenericCollection( Type type )
        {
            Type concurrentInterface = GetGenericInterface( type, typeof( IList<> ) );
            return ( concurrentInterface != null );
        }

        public static Type GetCollectionItemType( Type colectionType )
        {
            if ( TypeInfo.IsCollection( colectionType ) )
            {
                return typeof( object );
            }
            else if ( TypeInfo.IsGenericDictionary( colectionType ) )
            {
                Type concurrentInterface = GetGenericInterface( colectionType, typeof( IList<> ) );
                return concurrentInterface.GetGenericArguments()[ 0 ];
            }
            throw new ApplicationException( "colectionType is not a Collection type definition." );
        }

        public static bool IsDictionary( Type type )
        {
            if ( typeof( IDictionary ).IsAssignableFrom( type ) )
            {
                return true;
            }
            return false;
        }

        public static bool IsGenericDictionary( Type type )
        {
            Type concurrentInterface = GetGenericInterface( type, typeof( IDictionary<,> ) );
            return ( concurrentInterface != null );
        }

        public static Type GetDictionaryKeyType( Type dictionaryType )
        {
            if ( TypeInfo.IsDictionary( dictionaryType ) )
            {
                return typeof( object );
            }
            else if ( TypeInfo.IsGenericDictionary( dictionaryType ) )
            {
                Type concurrentInterface = GetGenericInterface( dictionaryType, typeof( IDictionary<,> ) );
                return concurrentInterface.GetGenericArguments()[ 0 ];
            }
            throw new ApplicationException( "dictionaryType is not a Dictionary type definition." );
        }

        public static Type GetDictionaryValueType( Type dictionaryType )
        {
            if ( TypeInfo.IsDictionary( dictionaryType ) )
            {
                return typeof( object );
            }
            else if ( TypeInfo.IsGenericDictionary( dictionaryType ) )
            {
                Type concurrentInterface = GetGenericInterface( dictionaryType, typeof( IDictionary<,> ) );
                return concurrentInterface.GetGenericArguments()[ 1 ];
            }
            throw new ApplicationException( "dictionaryType is not a Dictionary type definition." );
        }

        public static Type[] GetGenericArguments( Type type, Type constraintInterface )
        {
            Type concurrentInterface = GetGenericInterface( type, constraintInterface );
            return concurrentInterface.GetGenericArguments();
        }

        public static MethodInfo GetMethod( Type type, string methodName, Type constraintInterface )
        {
            Type concurrentInterface = TypeInfo.GetGenericInterface( type, constraintInterface );
            MethodInfo add = concurrentInterface.GetMethod( methodName );
            return add;
        }

        public static Type MakeGenericClass( Type type, Type[] arguments )
        {
            return type.MakeGenericType( arguments );
        }

        private static Type GetGenericInterface( Type type, Type constraintInterface )
        {
            Type[] _interfaces = type.GetInterfaces();
            foreach ( Type _interface in _interfaces )
            {
                if ( _interface.IsGenericType && _interface.GetGenericTypeDefinition() == constraintInterface )
                    return _interface;
            }
            return null;
        }
    }
}