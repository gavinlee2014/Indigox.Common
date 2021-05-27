using System;
using System.Collections.Generic;
using System.Reflection;

namespace Indigox.Common.NHibernateFactories
{
    public class SessionFactories
    {
        private static SessionFactories instance = new SessionFactories();
        private object _SessionFactoryLocker = new object();
        private object begin = new object();
        private object end = new object();

        private SessionFactories()
        {
        }

        public static SessionFactories Instance
        {
            get { return SessionFactories.instance; }
        }

        private Dictionary<string, NHibernateSessionFactory> factoriesMap = new Dictionary<string, NHibernateSessionFactory>();

        private Dictionary<string, Dictionary<string, string>> configs = new Dictionary<string, Dictionary<string, string>>();

        public void Register( string assemblyName, string configFilePath, string connectionStringName, bool autoBind )
        {
            lock ( _SessionFactoryLocker )
            {
                Dictionary<string, string> config = new Dictionary<string, string>();
                config.Add( "configFilePath", configFilePath );
                config.Add( "connectionStringName", connectionStringName );
                config.Add( "autoBind", Convert.ToString( autoBind ) );
                if ( this.configs.ContainsKey( assemblyName ) )
                {
                    this.configs[ assemblyName ] = config;
                }
                else
                {
                    this.configs.Add( assemblyName, config );
                }
            }
        }

        public NHibernateSessionFactory Get( Assembly assembly )
        {
            lock ( _SessionFactoryLocker )
            {
                string name = assembly.GetName().Name;
                return this.GetByName( name );
            }
        }

        private NHibernateSessionFactory GetByName( string assemblyName )
        {
            if ( !this.factoriesMap.ContainsKey( assemblyName ) )
            {
                if ( !this.configs.ContainsKey( assemblyName ) )
                {
                    throw new ApplicationException("没有配置 '" + assemblyName + "' 的数据库连接。");
                }

                Dictionary<string, string> config = this.configs[ assemblyName ];
                NHibernateSessionFactory factory = new NHibernateSessionFactory(
                        config[ "configFilePath" ],
                        config[ "connectionStringName" ],
                        Convert.ToBoolean( config[ "autoBind" ] )
                    );
                this.factoriesMap.Add( assemblyName, factory );
            }
            return this.factoriesMap[ assemblyName ];
        }

        public void InitSessions()
        {
            //Log.Debug( string.Format(
            //        "[{0}] beign SessionFactories.InitSessions...\r\n{1}",
            //        System.Threading.Thread.CurrentThread.ManagedThreadId,
            //        new System.Diagnostics.StackTrace( 0, true ).ToString()
            //    ) );

            lock ( this.begin )
            {
                foreach ( string key in this.configs.Keys )
                {
                    try
                    {
                        this.GetByName(key).InitCurrentSession();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException(string.Format("无法初始化 {0} 模块的数据库连接。", key), ex);
                    }
                }
            }
        }

        public void DisposeSessions()
        {
            //Log.Debug( string.Format(
            //        "[{0}] beign SessionFactories.DisposeSessions...\r\n{1}",
            //        System.Threading.Thread.CurrentThread.ManagedThreadId,
            //        new System.Diagnostics.StackTrace( 0, true ).ToString()
            //    ) );

            lock ( this.end )
            {
                foreach ( string key in this.factoriesMap.Keys )
                {
                    this.GetByName( key ).DisposeCurrentSession();
                }
            }
        }
    }
}