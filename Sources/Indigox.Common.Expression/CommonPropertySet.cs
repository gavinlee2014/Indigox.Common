using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Indigox.Common.Expression.Interface;

namespace Indigox.Common.Expression
{
    public class CommonPropertySet : IPropertySet
    {
        Dictionary<string, object > innerDictionary = new Dictionary<string, object>();

        public virtual int Count
        {
            get
            {
                return this.innerDictionary.Count;
            }
        }

        public virtual object this[ string name ]
        {
            get
            {
                return Get( name );
            }
            set
            {
                Set( name, value );
            }
        }

        public virtual object Get( string name )
        {
            if ( !innerDictionary.ContainsKey( name ) )
            {
                throw new KeyNotFoundException( "The property is retrieved and key does not exist in the set." );
            }
            return innerDictionary[ name ];
        }

        public virtual void Add( string name, object value )
        {
            if ( innerDictionary.ContainsKey( name ) )
            {
                throw new ArgumentException( "An element with the same key already exists in the set." );
            }
            innerDictionary.Add( name, value );
        }

        public virtual void Set( string name, object value )
        {
            if ( innerDictionary.ContainsKey( name ) )
            {
                innerDictionary[ name ] = value;
            }
            else
            {
                innerDictionary.Add( name, value );
            }
        }

        public virtual void Remove( string name )
        {
            if ( !innerDictionary.ContainsKey( name ) )
            {
                throw new KeyNotFoundException( "The property is retrieved and key does not exist in the set." );
            }
            innerDictionary.Remove( name );
        }

        public virtual void Clear()
        {
            innerDictionary.Clear();
        }

        public virtual bool Contains( string name )
        {
            return innerDictionary.ContainsKey( name );
        }
    }
}
