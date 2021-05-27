using System;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.NHibernateFactories;
using NHibernate;

namespace Indigox.Common.Membership.NHibernateImpl
{
    public class CacheManager
    {
        public static void ClearSecondLevelCache()
        {
            ISessionFactory sessionFactory = SessionFactories.Instance.Get( typeof( IPrincipal ).Assembly ).CurrentSessionFactory;
            sessionFactory.EvictQueries();
            foreach ( var collectionMetadata in sessionFactory.GetAllCollectionMetadata() )
            {
                sessionFactory.EvictCollection( collectionMetadata.Key );
            }
            foreach ( var classMetadata in sessionFactory.GetAllClassMetadata() )
            {
                sessionFactory.EvictEntity( classMetadata.Key );
            }
        }
    }
}