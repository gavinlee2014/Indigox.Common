using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.DomainModels.Repository.Interface;
using System.Collections;
using Indigox.Common.DomainModels.Queries;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Test.Repository.NHibernateImpl
{
    class NHibernateRepository<T> : IRepository<T>, IRepository
    {
        private static ObjectDAO<T> dao = new ObjectDAO<T>();

        public void Add( T item )
        {
            dao.Save( item );
        }

        public void Update( T item )
        {
            dao.Update( item );
        }

        public void Remove( T item )
        {
            dao.Delete( item );
        }

        public T Get( object key )
        {
            return dao.Get( key );
        }

        public T First( ISpecification spec )
        {
            IList<T> list = Find( Query.NewQuery.FindByCondition( spec ) );
            if ( list.Count > 0 )
                return list[ 0 ];
            return default( T );
        }

        public IList<T> Find( Query query )
        {
            return dao.Find( query );
        }

        #region no genric implement

        object IRepository.First( ISpecification spec )
        {
            return this.First( spec );
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

        #endregion


        object IRepository.Get(object key)
        {
            return this.Get(key);
        }
    }
}
