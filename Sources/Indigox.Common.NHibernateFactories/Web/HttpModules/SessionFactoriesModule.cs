using System;
using System.Web;
using Indigox.Common.Logging;

namespace Indigox.Common.NHibernateFactories.Web.HttpModules
{
    class SessionFactoriesModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init( HttpApplication application )
        {
            application.BeginRequest += new EventHandler( application_BeginRequest );
            application.EndRequest += new EventHandler( application_EndRequest );
        }

        void application_BeginRequest( object sender, EventArgs e )
        {
            try
            {
                SessionFactories.Instance.InitSessions();
            }
            catch ( Exception ex )
            {
                Log.Error( ex.ToString() );
            }
        }

        void application_EndRequest( object sender, EventArgs e )
        {
            try
            {
                SessionFactories.Instance.DisposeSessions();
            }
            catch ( Exception ex )
            {
                Log.Error( ex.ToString() );
            }
        }
    }
}
