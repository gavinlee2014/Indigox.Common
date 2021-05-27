using System;
using System.IO;
using Indigox.Common.Data.Configuration;
using Indigox.Common.Logging;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;

namespace Indigox.Common.NHibernateFactories
{
    public class NHibernateSessionFactory
    {
        private ISessionFactory _CurrentSessionFactory;
        private object _SessionFactoryLocker = new object();
        private bool autoBind;
        private string configFilePath;
        private string connectionStringName;

        public NHibernateSessionFactory( string configFilePath, string connectionStringName, bool autoBind )
        {
            this.configFilePath = configFilePath;
            this.connectionStringName = connectionStringName;
            this.autoBind = autoBind;
        }

        public ISessionFactory CurrentSessionFactory
        {
            get
            {
                if ( _CurrentSessionFactory == null )
                {
                    lock ( _SessionFactoryLocker )
                    {
                        if ( _CurrentSessionFactory == null )
                        {
                            ISessionFactory factory = BuildSessionFactory();
                            _CurrentSessionFactory = factory;
                        }
                        else
                        {
                            Log.Debug( "Try to reconfigure nhibernate from file: " + configFilePath );
                        }
                    }
                }
                return _CurrentSessionFactory;
            }
        }

        public void BeginTransaction()
        {
            BeginTransaction( GetCurrentSession() );
        }

        public void DisposeCurrentSession()
        {
            if ( this.autoBind )
            {
                ISession session = TryGetCurrentSession();

                if ( session == null )
                {
                    Log.Error( "NHiberate current session is null. (" + configFilePath + ")" );
                }
                else
                {
                    this.TryCommitTransaction( session );
                    this.TryUnbindCurrentSession();
                }
            }
        }

        public ISession GetCurrentSession()
        {
            ISession current = null;
            if ( CurrentSessionContext.HasBind( CurrentSessionFactory ) )
            {
                current = CurrentSessionFactory.GetCurrentSession();
            }
            else
            {
                // 因计算 AclCache 需要每次打开新的 session
                Log.Debug( "GetCurrentSession opened an new session. (" + configFilePath + ")" );
                current = OpenSession();
            }
            return current;
        }

        public void InitCurrentSession()
        {
            if ( this.autoBind )
            {
                ISession session = this.OpenSession();
                this.BindSession( session );
                this.BeginTransaction( session );
            }
        }

        public ISession OpenSession()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            ISession session = null;

            /*
             * 打开一个标准的 session
             */
            // session = CurrentSessionFactory.OpenSession();

            /*
             * 打开一个提交事务时自动关闭数据库连接的 session
             * 
             * 因为 security 在关闭事务时没有关闭 session，造成数据库连接
             * 没有释放，所以在这里创建自动关闭连接的 session，保证数据库
             * 连接及时释放
             * 
             * 造成此问题的原因是由于领域层没有处理事务，当领域层实现事务
             * 控制，打开标准的 session 就可以了
             */
            ISessionFactoryImplementor factory = (ISessionFactoryImplementor)CurrentSessionFactory;
            session = factory.OpenSession(
                null,
                factory.Settings.IsFlushBeforeCompletionEnabled,
                true,
                factory.Settings.ConnectionReleaseMode );

            watch.Stop();
            Log.Debug(string.Format("Open NHibernate session spend: {0}ms.", watch.Elapsed.TotalMilliseconds));

            return session;
        }

        public void TryCommitTransaction()
        {
            TryCommitTransaction( GetCurrentSession() );
        }

        private void BeginTransaction( ISession session )
        {
            session.Transaction.Begin();
        }

        private void BindSession( ISession session )
        {
            NHibernate.Context.CurrentSessionContext.Bind( session );
        }

        private ISessionFactory BuildSessionFactory()
        {
            NHibernate.Cfg.Configuration cfg;

            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

            // load configuration file
            string configFile = GetConfigFilePath();
            try
            {
                Log.Info("Load nhibernate configuration from: " + configFile);
                cfg = new NHibernate.Cfg.Configuration().Configure(configFile);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("配置文件 '" + this.configFilePath + "' 错误，无法创建会话。", ex);
            }

            try
            {
                // set connection
                cfg.Properties["connection.connection_string"] =
                    DatabaseSection.Default.Connections[this.connectionStringName].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("缺少数据库连接 '" + this.connectionStringName + "' 的配置，无法创建会话。", ex);
            }

            // build session factory
            ISessionFactory factory = cfg.BuildSessionFactory();

            Log.Debug(string.Format("BuildSessionFactory '{1}' spend: {0}ms", watch.Elapsed.TotalMilliseconds, this.configFilePath));

            return factory;
        }

        private string GetConfigFilePath()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine( baseDir, configFilePath );
            return fullPath;
        }

        private void TryCommitTransaction( ISession session )
        {
            if ( !IsTransactionActive( session ) )
            {
                Log.Debug( "No active transaction. (" + configFilePath + ")" );
            }
            else
            {
                try
                {
                    session.Transaction.Commit();
                    session.Flush();
                }
                catch ( Exception commitExx )
                {
                    Log.Error( "NHiberateSession Error (" + configFilePath + ") : Commit Transaction failed.\r\n" + commitExx.ToString() );
                    try
                    {
                        session.Transaction.Rollback();
                    }
                    catch ( Exception rollbackEx )
                    {
                        Log.Error( "NHiberateSession Error (" + configFilePath + ") : Rollback Transaction failed.\r\n" + rollbackEx.ToString() );
                    }
                }
            }
        }

        public bool IsTransactionActive( ISession session )
        {
            return ( session.Transaction != null ) && ( session.Transaction.IsActive );
        }

        private ISession TryGetCurrentSession()
        {
            ISession session = null;
            try
            {
                session = CurrentSessionFactory.GetCurrentSession();
            }
            catch ( Exception ex )
            {
                Log.Error( "TryGetCurrentSession failed. " + ex.ToString() );
            }
            return session;
        }

        private void TryUnbindCurrentSession()
        {
            try
            {
                ISession session = this.UnbindSession();
                session.Dispose();
            }
            catch ( Exception ex )
            {
                Log.Error( "Unbind NHibernate current session occurs error. (" + configFilePath + ")" );
                Log.Error( ex.ToString() );
            }
        }

        private ISession UnbindSession()
        {
            ISession session = NHibernate.Context.CurrentSessionContext.Unbind( CurrentSessionFactory );
            return session;
        }

        #region useful for test

        public void ClearSession()
        {
            Log.Debug( "clear current session." );
            ISession session = GetCurrentSession();
            if ( session != null )
            {
                session.Clear();
            }
        }

        public void FlushSession()
        {
            ISession session = GetCurrentSession();
            session.Flush();
        }

        public void ResetSession()
        {
            Log.Debug( "reset current session." );
            ClearSession();
            UnbindSession();
            BindSession( OpenSession() );
        }

        public bool SessionIsDirty()
        {
            ISession session = GetCurrentSession();
            return session.IsDirty();
        }

        #endregion useful for test
    }
}