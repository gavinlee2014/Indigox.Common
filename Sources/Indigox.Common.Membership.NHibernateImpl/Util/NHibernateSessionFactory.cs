using System;
using System.IO;
using System.Web;
using Indigox.Common.Data.Configuration;
using Indigox.Common.Logging;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;

namespace Indigox.Common.Membership.NHibernateImpl.Utils
{
    public class NHibernateSessionFactory
    {
        private static readonly string _ConfigFilePath = "Config\\membership.nhb.cfg.xml";
        private static ISessionFactory _CurrentSessionFactory;
        private static object _SessionFactoryLocker = new object();

        public static ISessionFactory CurrentSessionFactory
        {
            get
            {
                if ( _CurrentSessionFactory == null )
                    lock ( _SessionFactoryLocker )
                        if ( _CurrentSessionFactory == null )
                        {
                            // load configuration file
                            string configFile = GetConfigFilePath();
                            Log.Info( "Load nhibernate configuration from: " + configFile );
                            NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration().Configure( configFile );

                            // set connection
                            Log.Info( "set connection as BPM connection string." );
                            cfg.Properties[ "connection.connection_string" ] = DatabaseSection.Default.Connections[ "BPM" ].ConnectionString;

                            // set session context class
                            if ( IsWebApp() )
                            {
                                Log.Info( "set current session context class as WebSessionContext." );
                                cfg.Properties[ "current_session_context_class" ] = "web";
                            }
                            else
                            {
                                Log.Info( "set current session context class as CallSessionContext." );
                                cfg.Properties[ "current_session_context_class" ] = "call";
                            }

                            // build session factory
                            _CurrentSessionFactory = cfg.BuildSessionFactory();
                        }
                        else
                        {
                            Log.Debug( "Try to reconfigure nhibernate from file: " + _ConfigFilePath );
                        }
                return _CurrentSessionFactory;
            }
        }

        private static bool IsWebApp()
        {
            return HttpContext.Current != null;
        }

        private static string GetConfigFilePath()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine( baseDir, _ConfigFilePath );
            return fullPath;
        }

        public static ISession GetCurrentSession()
        {
            return CurrentSessionFactory.GetCurrentSession();
        }

        public static ISession OpenSession()
        {
            return CurrentSessionFactory.OpenSession();
        }

        public static void BindSession( ISession session )
        {
            NHibernate.Context.CurrentSessionContext.Bind( session );
        }

        public static ISession UnbindSession()
        {
            ISession session = NHibernate.Context.CurrentSessionContext.Unbind( CurrentSessionFactory );
            return session;
        }

        public static void ClearSecondLevelCache()
        {
            //NHibernateSessionFactory.CurrentSessionFactory.EvictCollection( "Indigox.Common.Membership.Interfaces.IPrincipal" );
            NHibernateSessionFactory.CurrentSessionFactory.EvictCollection( "Indigox.Common.Membership.Group.Members" );
            //NHibernateSessionFactory.CurrentSessionFactory.EvictCollection( "Indigox.Common.Membership.RelativePosition" );
            //NHibernateSessionFactory.CurrentSessionFactory.EvictCollection( "Indigox.Common.Membership.RelativePosition" );
        }

        #region useful for test

        public static bool SessionIsDirty()
        {
            ISession session = GetCurrentSession();
            return session.IsDirty();
        }

        public static void FlushSession()
        {
            ISession session = GetCurrentSession();
            session.Flush();
        }

        public static void ClearSession()
        {
            Log.Debug( "clear current session." );
            ISession session = GetCurrentSession();
            session.Clear();
        }

        public static void ResetSession()
        {
            Log.Debug( "reset current session." );
            ClearSession();
            UnbindSession();
            BindSession( OpenSession() );
        }

        #endregion

        #region private methods

        private static ICurrentSessionContext GetCurrentSessionContext( ISessionFactory sessionFactory )
        {
            return ( (ISessionFactoryImplementor)sessionFactory ).CurrentSessionContext;
        }

        #endregion
    }
}
