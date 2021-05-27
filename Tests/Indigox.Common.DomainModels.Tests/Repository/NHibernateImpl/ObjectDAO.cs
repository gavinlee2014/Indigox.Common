using System.Collections.Generic;
using Indigox.Common.DomainModels.Queries;
using NHibernate;
using NHibernate.Criterion;

namespace Indigox.Common.DomainModels.Test.Repository.NHibernateImpl
{
    class ObjectDAO<T>
    {
        public void Save( T entity )
        {
            ISession session = NHibernateSessionFactory.OpenSession();
            //using ( ISession session = NHibernateSessionFactory.OpenSession() )
            using ( ITransaction transaction = session.BeginTransaction() )
            {
                session.Save( entity );
                transaction.Commit();
            }
            //session.Clear();
        }

        public void Update( T entity )
        {
            ISession session = NHibernateSessionFactory.OpenSession();
            //using ( ISession session = NHibernateSessionFactory.OpenSession() )
            using ( ITransaction transaction = session.BeginTransaction() )
            {
                session.Update( entity );
                transaction.Commit();
            }
            //session.Clear();
        }

        public void SaveOrUpdate( T entity )
        {
            ISession session = NHibernateSessionFactory.OpenSession();
            //using ( ISession session = NHibernateSessionFactory.OpenSession() )
            using ( ITransaction transaction = session.BeginTransaction() )
            {
                session.SaveOrUpdate( entity );
                transaction.Commit();
            }
            //session.Clear();
        }

        public void Delete( T entity )
        {
            ISession session = NHibernateSessionFactory.OpenSession();
            //using ( ISession session = NHibernateSessionFactory.OpenSession() )
            using ( ITransaction transaction = session.BeginTransaction() )
            {
                session.Delete( entity );
                transaction.Commit();
            }
            //session.Clear();
        }

        public T Get( object key )
        {
            ISession session = NHibernateSessionFactory.OpenSession();
            //using ( ISession session = NHibernateSessionFactory.OpenSession() )
            {
                //
                // To solve subclass problem.
                // When get the base class instance, the Load method will return an instnace
                // as an proxy type of the base type, not the expected type.
                // So must use Get method here.
                //
                T item = session.Get<T>( key );
                //session.Clear();
                return item;
            }
        }

        public IList<T> Find( Query query )
        {
            ISession session = NHibernateSessionFactory.OpenSession();
            //using ( ISession session = NHibernateSessionFactory.OpenSession() )
            {
                ICriterion expression = SpecificationConvert.GetNHibernateCriterion( query.Specifications );
                ICriteria criteria = session.CreateCriteria( typeof( T ) )
                    .Add( expression )
                    .SetFirstResult( query.Start );

                if ( query.Limit >= 0 )
                {
                    criteria.SetMaxResults( query.Limit );
                }
                foreach ( OrderBy orderBy in query.SortOrder )
                {
                    Order order = new Order( orderBy.OrderByField, ( orderBy.SortOrder == OrderType.Asc ) );
                    criteria.AddOrder( order );
                }
                //session.Clear();
                return criteria.List<T>();
            }
        }
    }
}
