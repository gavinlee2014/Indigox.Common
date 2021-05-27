using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Indigox.Common.NHibernate.Extension.Reflection
{
    public class DictionaryAccessor
    {
        public static void Add( object dictionary, object key, object value )
        {
            Type dictionaryType = dictionary.GetType();

            if ( TypeInfo.IsDictionary( dictionaryType ) )
            {
                ( (IDictionary)dictionary ).Add( key, value );
            }
            else if ( TypeInfo.IsGenericDictionary( dictionaryType ) )
            {
                MethodInfo add = TypeInfo.GetMethod( dictionaryType, "Add", typeof( IDictionary<,> ) );
                add.Invoke( dictionary, new object[] { key, value } );
            }
        }

        public static void ForEach( object dictionary, DictionaryForeachHandler handler )
        {
            IEnumerable em = dictionary as IEnumerable;

            Type dictionaryType = dictionary.GetType();

            bool isGenericDictionary = TypeInfo.IsGenericDictionary( dictionaryType );

            Type entryType = TypeInfo.MakeGenericClass(
                typeof( KeyValuePair<,> ),
                TypeInfo.GetGenericArguments( dictionaryType, typeof( IDictionary<,> ) ) );

            foreach ( object item in em )
            {
                if ( item is DictionaryEntry )
                {
                    DictionaryEntry de = (DictionaryEntry)item;
                    handler.Invoke( de.Key, de.Value );
                }
                else if ( isGenericDictionary )
                {
                    object key = ObjectAccessor.GetProperty( item, "Key", entryType );
                    object value = ObjectAccessor.GetProperty( item, "Value", entryType );
                    handler.Invoke( key, value );
                }
                else
                {
                    // error
                }
            }
        }
    }

    public delegate void DictionaryForeachHandler( object key, object value );
}