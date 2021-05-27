using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Indigox.Common.NHibernate.Extension.Reflection
{
    public class CollectionAccessor
    {
        public static void Add( object collection, object item )
        {
            Type collectionType = collection.GetType();

            if ( TypeInfo.IsCollection( collectionType ) )
            {
                ( (IList)collection ).Add( item );
            }
            else if ( TypeInfo.IsGenericCollection( collectionType ) )
            {
                MethodInfo add = TypeInfo.GetMethod( collectionType, "Add", typeof( IList<> ) );
                add.Invoke( collection, new object[] { item } );
            }
        }

        public static void ForEach( object collection, CollectionForeachHandler handler )
        {
            IEnumerable em = collection as IEnumerable;
            int i = 0;
            foreach ( object item in em )
            {
                handler.Invoke( item, i++ );
            }
        }
    }

    public delegate void CollectionForeachHandler( object item, int index );
}