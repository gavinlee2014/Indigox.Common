using System;
using System.Collections;
using System.Collections.Generic;

namespace Indigox.Common.Utilities
{
    public class CollectionUtil
    {
        public static List<T> ConvertToList<T>( IEnumerable e )
        {
            List<T> list = new List<T>();
            foreach ( T item in e )
            {
                list.Add( item );
            }
            return list;
        }

        public static T[] ConvertToArray<T>( IEnumerable e )
        {
            return ConvertToList<T>( e ).ToArray();
        }

        [Obsolete( "instead by CopyTo." )]
        public static void Copy<T>( ICollection<T> source, ICollection<T> target )
        {
            foreach ( T item in source )
            {
                target.Add( item );
            }
        }

        public static void CopyTo<T>( ICollection<T> source, ICollection<T> target )
        {
            foreach ( T item in source )
            {
                target.Add( item );
            }
        }
    }
}
