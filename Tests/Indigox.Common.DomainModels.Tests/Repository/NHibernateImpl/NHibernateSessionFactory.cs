using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using NHibernate;
using NHibernate.Context;

namespace Indigox.Common.DomainModels.Test.Repository.NHibernateImpl
{
    class NHibernateSessionFactory
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    string configFile = GetConfigFilePath();
                    var cfg = new NHibernate.Cfg.Configuration().Configure(configFile);
                    _sessionFactory = cfg.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        static readonly string configFilePath = "Repository\\Config\\hibernate.cfg.xml";

        private static string GetConfigFilePath()
        {
            string baseDir = Path.GetDirectoryName(Assembly.GetCallingAssembly().CodeBase);
            string fullPath = Path.Combine(baseDir, configFilePath);
            return fullPath;
        }

        public static ISession OpenSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
            {
                Console.WriteLine("create new nhibernate session, and bind to current session context.");
                CurrentSessionContext.Bind(SessionFactory.OpenSession());
            }
            return SessionFactory.GetCurrentSession();
        }
    }
}
