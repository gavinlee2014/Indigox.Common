using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Indigox.Common.Utilities.Collections
{
    public class KeyedList<TKey, TItem> : KeyedCollection<TKey, TItem>
    {
        private string keyPropertyName;

        public new IDictionary<TKey, TItem> Dictionary { get { return base.Dictionary; } }

        public KeyedList( string keyPropertyName )
            : base()
        {
            this.keyPropertyName = keyPropertyName;
        }

        public KeyedList( string keyPropertyName, IEqualityComparer<TKey> comparer )
            : base( comparer )
        {
            this.keyPropertyName = keyPropertyName;
        }

        public KeyedList( string keyPropertyName, IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold )
            : base( comparer, dictionaryCreationThreshold )
        {
            this.keyPropertyName = keyPropertyName;
        }

        protected override TKey GetKeyForItem( TItem item )
        {
            return (TKey)ReflectUtil.GetProperty( item, keyPropertyName );
        }
    }
}
