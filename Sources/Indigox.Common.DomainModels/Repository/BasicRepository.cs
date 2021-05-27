using System;
using System.Collections;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Events;
using Indigox.Common.DomainModels.Interface.Entity;
using Indigox.Common.DomainModels.Interface.Specifications;
using Indigox.Common.DomainModels.Queries;
using Indigox.Common.DomainModels.Repository.Interface;
using Indigox.Common.EventBus;
using Indigox.Common.Logging;
using Indigox.Common.Utilities;

namespace Indigox.Common.DomainModels.Repository
{
    public class BasicRepository<T> : IRepository<T>, IRepository
    {
        public virtual T Get( object key )
        {
            string identifier = key == null ? "" : key.ToString();
            if (BasicRepositoryCache.Instance.ContainsKey(identifier))
            {
                return (T)BasicRepositoryCache.Instance[identifier];
            }
            return default( T );
        }

        public T First( Query query )
        {
            IList<T> list = Find( query );
            if ( list.Count > 0 )
                return list[ 0 ];
            return default( T );
        }

        public virtual IList<T> Find( Query query )
        {
            ISpecification spec = query.Specifications;
            IList<T> sortResult = new List<T>();
            IList<T> result = new List<T>();
            IList<T> raw = new List<T>();
            foreach ( object item in BasicRepositoryCache.Instance.Values )
            {
                if ( typeof( T ).IsAssignableFrom( item.GetType() ) )
                {
                    raw.Add( (T)item );
                }
            }
            foreach ( T item in raw )
            {
                if ( spec.IsStatisfiedBy( item ) )
                {
                    sortResult.Add( item );
                }
            }
            if ( query.Start < sortResult.Count )
            {
                int resultSize = ( query.Limit < 0 ? sortResult.Count : query.Limit );
                for ( int i = query.Start; i < resultSize; i++ )
                {
                    if ( i < sortResult.Count )
                    {
                        result.Add( sortResult[ i ] );
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result;
        }

        public virtual int GetTotalCount(Query query)
        {
            var list = Find( query );
            return list.Count;
        }

        public void Add( T model )
        {
            EventTrigger.Trigger( model, new RepositoryAddEvent() );
            DoAdd( model );
            EventTrigger.Trigger( model, new RepositoryAddedEvent() );
        }

        protected virtual void DoAdd( T model )
        {
            ArgumentAssert.IsTypeOf( typeof( IEntity ), model, "model" );

            IEntity domainObject = (IEntity)model;
            object identifier = domainObject.GetObjectIdentity().Identifier;

            Log.Debug( "Add DomainObject with identifier :" + identifier.ToString() );

            if ( !BasicRepositoryCache.Instance.ContainsKey( identifier ) )
            {
                BasicRepositoryCache.Instance.Add( identifier, model );
            }
            else
            {
                BasicRepositoryCache.Instance[ identifier ] = model;
            }
        }

        public void Update( T model )
        {
            EventTrigger.Trigger( model, new RepositoryUpdateEvent() );
            DoUpdate( model );
            EventTrigger.Trigger( model, new RepositoryUpdatedEvent() );
        }

        protected virtual void DoUpdate( T model )
        {
            ArgumentAssert.IsTypeOf( typeof( IEntity ), model, "model" );

            object identifier = ( (IEntity)model ).GetObjectIdentity().Identifier;

            Log.Debug( "Update DomainObject with identifier :" + identifier.ToString() );

            if ( BasicRepositoryCache.Instance.ContainsKey( identifier ) )
            {
                BasicRepositoryCache.Instance[ identifier ] = model;
            }
            else
            {
                throw new Exception( "Can not find the object in BasicRepository! Identifier is :" + identifier.ToString() );
            }
        }

        public void Remove( T model )
        {
            EventTrigger.Trigger( model, new RepositoryDeleteEvent() );
            DoRemove( model );
            EventTrigger.Trigger( model, new RepositoryDeletedEvent() );
        }

        protected virtual void DoRemove( T model )
        {
            ArgumentAssert.IsTypeOf( typeof( IEntity ), model, "model" );

            object identifier = ( (IEntity)model ).GetObjectIdentity().Identifier;

            if ( !BasicRepositoryCache.Instance.ContainsKey( identifier ) )
            {
                BasicRepositoryCache.Instance.Remove( identifier );
            }
            else
            {
                throw new Exception( "Can not find the object in BasicRepository! Identifier is :" + identifier.ToString() );
            }
        }

        private bool IsDomainObject( Type type )
        {
            object[] attrs = type.GetCustomAttributes( typeof( DomainObjectAttribute ), true );
            if ( attrs.Length == 1 )
            {
                return true;
            }
            return false;
        }

        private object GetKeyValue( Type type, object instance )
        {
            object[] attrs = type.GetCustomAttributes( typeof( DomainObjectAttribute ), true );
            if ( attrs.Length == 1 )
            {
                DomainObjectAttribute domainObjectAttr = (DomainObjectAttribute)attrs[ 0 ];
                if ( domainObjectAttr.Keys == null || domainObjectAttr.Keys.Length == 0 )
                {
                    throw new Exception( "请至少需要设置一个属性作为主键" );
                }
                if ( domainObjectAttr.Keys.Length == 1 )
                {
                    return ReflectUtil.GetProperty( instance, domainObjectAttr.Keys[ 0 ] );
                }
                else
                {
                    throw new Exception( "不支持多属性作为主键" );
                }
            }
            throw new Exception( "类 " + type.FullName + " 不是领域对象类，无法获取主键" );
        }

        object IRepository.Get( object key )
        {
            return this.Get( key );
        }

        object IRepository.First( Query query )
        {
            return this.First( query );
        }

        IList IRepository.Find( Query query )
        {
            return (IList)this.Find( query );
        }

        void IRepository.Add( object model )
        {
            this.Add( (T)model );
        }

        void IRepository.Update( object model )
        {
            this.Update( (T)model );
        }

        void IRepository.Remove( object model )
        {
            this.Remove( (T)model );
        }
    }
}