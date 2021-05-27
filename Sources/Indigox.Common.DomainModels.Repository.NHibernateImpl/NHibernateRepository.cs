using System;
using System.Collections;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Queries;
using Indigox.Common.DomainModels.Repository.Interface;
using Indigox.Common.DomainModels.Interface.Entity;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl
{
    public class NHibernateRepository<T> : BasicRepository<T>
    {
        private static NHibernateDAO<T> dao = new NHibernateDAO<T>();

        protected override void DoAdd( T item )
        {
            dao.Save( item );
        }

        protected override void DoUpdate( T item )
        {
            dao.Update( item );
        }

        protected override void DoRemove( T item )
        {
            dao.Delete( item );
        }

        public override T Get( object key )
        {
            return dao.Get( key );
        }

        public override IList<T> Find( Query query )
        {
            return dao.Find( query );
        }

        public override int GetTotalCount(Query query)
        {
            return dao.GetTotalCount(query);
        }
    }
}
