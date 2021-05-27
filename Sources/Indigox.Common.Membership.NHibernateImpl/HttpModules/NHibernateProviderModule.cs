using System;
using System.Web;
using Indigox.Common.Membership.NHibernateImpl.Utils;

namespace Indigox.Common.Membership.NHibernateImpl.HttpModules
{
    public class NHibernateProviderModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init( HttpApplication application )
        {
            application.BeginRequest += new EventHandler( OnBeginRequest );
            application.EndRequest += new EventHandler( OnEndRequest );
        }

        void OnBeginRequest( object sender, EventArgs e )
        {
            // bind bpm nhibernate session
            var session = NHibernateSessionFactory.OpenSession();
            NHibernateSessionFactory.BindSession( session );
            session.BeginTransaction();
        }

        void OnEndRequest( object sender, EventArgs e )
        {
            // unbind bpm nhibernate session
            var session = NHibernateSessionFactory.UnbindSession();
            if ( session != null )
            {
                try
                {
                    if ( session.IsOpen && session.Transaction.IsActive )
                    {
                        session.Transaction.Commit();
                    }
                }
                catch ( Exception )
                {
                    session.Transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Dispose();
                }
            }
        }
    }
}
