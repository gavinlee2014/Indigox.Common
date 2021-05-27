using System;
using NHibernate;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl
{
    internal class NHibernateSessionFactory
    {
        public static ISessionFactory CurrentSessionFactory
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static ISession GetCurrentSession()
        {
            throw new NotImplementedException();
        }

        public static ISession OpenSession()
        {
            throw new NotImplementedException();
        }

        public static void BindSession( ISession session )
        {
            throw new NotImplementedException();
        }

        public static ISession UnbindSession()
        {
            throw new NotImplementedException();
        }

        internal static void ClearSession()
        {
            throw new NotImplementedException();
        }

        internal static void ResetSession()
        {
            throw new NotImplementedException();
        }
    }
}
