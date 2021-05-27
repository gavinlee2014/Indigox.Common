using System;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Queries;
using Indigox.Common.Logging;
using NHibernate;
using NHibernate.Criterion;
using Indigox.Common.NHibernateFactories;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl
{
    class NHibernateDAO<T>
    {
        public void Save( T process )
        {
            NHibernateSessionFactory factory = SessionFactories.Instance.Get( typeof( T ).Assembly );

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            TimeSpan waitForLock, querySpend;

            lock (SQLReadWriteLocker)
            {
                waitForLock = stopwatch.Elapsed;
                stopwatch.Reset();

                ISession session = factory.GetCurrentSession();
                bool success = false;
                try
                {
                    session.Save( process );
                    success = true;
                }
                finally
                {
                    if ( !success )
                    {
                        factory.ClearSession();
                        factory.ResetSession();
                    }

                    querySpend = stopwatch.Elapsed;
                    Log.Debug(string.Format("NHibernate save <{1}> spend: {0}ms, wait for other spend: {2}ms",
                                            querySpend.TotalMilliseconds,
                                            typeof(T).FullName,
                                            waitForLock.TotalMilliseconds));
                }
            }
        }

        public void Update( T process )
        {
            NHibernateSessionFactory factory = SessionFactories.Instance.Get( typeof( T ).Assembly );

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            TimeSpan waitForLock, querySpend;

            lock (SQLReadWriteLocker)
            {
                waitForLock = stopwatch.Elapsed;
                stopwatch.Reset();

                ISession session = factory.GetCurrentSession();
                bool success = false;
                try
                {
                    session.Update( process );
                    success = true;
                }
                finally
                {
                    if ( !success )
                    {
                        factory.ClearSession();
                        factory.ResetSession();
                    }

                    querySpend = stopwatch.Elapsed;
                    Log.Debug(string.Format("NHibernate update <{1}> spend: {0}ms, wait for other spend: {2}ms",
                                            querySpend.TotalMilliseconds,
                                            typeof(T).FullName,
                                            waitForLock.TotalMilliseconds));
                }
            }
        }

        public void SaveOrUpdate( T process )
        {
            NHibernateSessionFactory factory = SessionFactories.Instance.Get( typeof( T ).Assembly );

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            TimeSpan waitForLock, querySpend;

            lock (SQLReadWriteLocker)
            {
                waitForLock = stopwatch.Elapsed;
                stopwatch.Reset();

                ISession session = factory.GetCurrentSession();
                bool success = false;
                try
                {
                    session.SaveOrUpdate( process );
                    success = true;
                }
                finally
                {
                    if ( !success )
                    {
                        factory.ClearSession();
                        factory.ResetSession();
                    }

                    querySpend = stopwatch.Elapsed;
                    Log.Debug(string.Format("NHibernate saveOrUpdate <{1}> spend: {0}ms, wait for other spend: {2}ms",
                                            querySpend.TotalMilliseconds,
                                            typeof(T).FullName,
                                            waitForLock.TotalMilliseconds));
                }
            }
        }

        public void Delete( T process )
        {
            NHibernateSessionFactory factory = SessionFactories.Instance.Get( typeof( T ).Assembly );

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            TimeSpan waitForLock, querySpend;

            lock (SQLReadWriteLocker)
            {
                waitForLock = stopwatch.Elapsed;
                stopwatch.Reset();

                ISession session = factory.GetCurrentSession();
                bool success = false;
                try
                {
                    session.Delete( process );
                    success = true;
                }
                finally
                {
                    if ( !success )
                    {
                        factory.ClearSession();
                        factory.ResetSession();
                    }

                    querySpend = stopwatch.Elapsed;
                    Log.Debug(string.Format("NHibernate delete <{1}> spend: {0}ms, wait for other spend: {2}ms",
                                            querySpend.TotalMilliseconds,
                                            typeof(T).FullName,
                                            waitForLock.TotalMilliseconds));
                }
            }
        }

        public T Get( object key )
        {
            NHibernateSessionFactory factory = SessionFactories.Instance.Get( typeof( T ).Assembly );

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            TimeSpan waitForLock, querySpend;

            lock (SQLReadWriteLocker)
            {
                waitForLock = stopwatch.Elapsed;
                stopwatch.Reset();

                ISession session = factory.GetCurrentSession();
                bool success = false;
                try
                {
                    bool autoBeginTransaction = !factory.IsTransactionActive( session );
                    if ( autoBeginTransaction )
                    {
                        factory.BeginTransaction();
                    }

                    /*
                     * To solve subclass problem.
                     * When get the base class instance, the Load method will return an instnace
                     * as an proxy type of the base type, not the expected type.
                     * So must use Get method here.
                     */
                    T result = (T)session.Get( InstanceTypeMapping.GetMappedClass<T>(), key );

                    if ( autoBeginTransaction )
                    {
                        factory.TryCommitTransaction();
                    }

                    success = true;

                    return result;
                }
                finally
                {
                    if ( !success )
                    {
                        factory.ClearSession();
                        factory.ResetSession();
                    }

                    querySpend = stopwatch.Elapsed;
                    Log.Debug(string.Format("NHibernate get <{1}> spend: {0}ms, wait for other spend: {2}ms",
                                            querySpend.TotalMilliseconds,
                                            typeof(T).FullName,
                                            waitForLock.TotalMilliseconds));
                }
            }
        }

        public IList<T> Find( Query query )
        {
            NHibernateSessionFactory factory = SessionFactories.Instance.Get( typeof( T ).Assembly );

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            TimeSpan waitForLock, querySpend;

            lock ( SQLReadWriteLocker )
            {
                waitForLock = stopwatch.Elapsed;
                stopwatch.Reset();

                ISession session = factory.GetCurrentSession();
                bool success = false;
                try
                {
                    ICriteria criteria = session.CreateCriteria( InstanceTypeMapping.GetMappedClass<T>() );
                    if ( query.Specifications != null )
                    {
                        SpecificationConvert.GetNHibernateCriterion<T>( criteria, query.Specifications );
                    }
                    criteria.SetFirstResult( query.Start );
                    if ( query.Limit >= 0 )
                    {
                        criteria.SetMaxResults( query.Limit );
                    }
                    foreach ( OrderBy orderBy in query.SortOrder )
                    {
                        Order order = new Order( orderBy.OrderByField, ( orderBy.SortOrder == OrderType.Asc ) );
                        criteria.AddOrder( order );
                    }

                    bool autoBeginTransaction = !factory.IsTransactionActive( session );
                    if ( autoBeginTransaction )
                    {
                        factory.BeginTransaction();
                    }

                    IList<T> result = criteria.List<T>();

                    if ( autoBeginTransaction )
                    {
                        factory.TryCommitTransaction();
                    }

                    success = true;

                    return result;
                }
                finally
                {
                    if ( !success )
                    {
                        factory.ClearSession();
                        factory.ResetSession();
                    }

                    querySpend = stopwatch.Elapsed;
                    Log.Debug(string.Format("NHibernate find <{1}> spend: {0}ms, wait for other spend: {2}ms",
                                            querySpend.TotalMilliseconds,
                                            typeof(T).FullName,
                                            waitForLock.TotalMilliseconds));
                }
            }
        }

        public int GetTotalCount( Query query )
        {
            NHibernateSessionFactory factory = SessionFactories.Instance.Get( typeof( T ).Assembly );

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            TimeSpan waitForLock, querySpend;

            lock (SQLReadWriteLocker)
            {
                waitForLock = stopwatch.Elapsed;
                stopwatch.Reset();

                ISession session = factory.GetCurrentSession();
                bool success = false;
                try
                {
                    ICriteria criteria = session.CreateCriteria( InstanceTypeMapping.GetMappedClass<T>() );
                    criteria.SetProjection( Projections.RowCount() );
                    if ( query.Specifications != null )
                    {
                        SpecificationConvert.GetNHibernateCriterion<T>( criteria, query.Specifications );
                    }

                    bool autoBeginTransaction = !factory.IsTransactionActive( session );
                    if ( autoBeginTransaction )
                    {
                        factory.BeginTransaction();
                    }

                    int result = criteria.UniqueResult<int>();

                    if ( autoBeginTransaction )
                    {
                        factory.TryCommitTransaction();
                    }

                    success = true;

                    return result;
                }
                finally
                {
                    if ( !success )
                    {
                        factory.ClearSession();
                        factory.ResetSession();
                    }

                    querySpend = stopwatch.Elapsed;
                    Log.Debug(string.Format("NHibernate get total count <{1}> spend: {0}ms, wait for other spend: {2}ms",
                                            querySpend.TotalMilliseconds,
                                            typeof(T).FullName,
                                            waitForLock.TotalMilliseconds));
                }
            }
        }

        //private static object SQLWriteLocker = new object();
        private static object SQLReadWriteLocker = new object();
    }
}
