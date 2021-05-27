using System;
using System.Collections.ObjectModel;

namespace Indigox.Common.Configuration
{
    public abstract class ConfigurationsCollection<T> : KeyedCollection<string, T>
    {
        //protected Dictionary<string, T> Configs
        //{
        //    get { return configs; }
        //}

        protected ConfigurationsCollection()
        {
        }

        public void Register( T config )
        {
            string key = this.GetKeyForItem( config );
            if ( !this.Contains( key ) )
            {
                this.Add( config );
            }
            else
            {
                //throw new Exception( "The item is already exists in collection." );
                if ( object.ReferenceEquals( base.Dictionary[ key ], config ) )
                {
                    base.ChangeItemKey( config, GetKeyForItem( config ) );
                }
            }
        }

        public T GetConfiguration( string key )
        {
            return this[ key ];
        }

        public T GetConfiguration()
        {
            if ( this.Count == 1 )
            {
                return base[ 0 ];
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}