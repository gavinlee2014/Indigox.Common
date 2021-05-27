using System;
using System.Threading;
using System.Web;
using Indigox.Common.Logging;
using Indigox.Common.NHibernateFactories.Configuration;
using Indigox.Common.StateContainer;

namespace Indigox.Common.NHibernateFactories
{
    internal class NHibernateStateContextListener : StateContextListener
    {
        [ThreadStatic]
        private static int count = 0;

        public override void OnApplicationBegin( IApplicationState application )
        {
            var configurator = new XmlConfigurator( "config\\factories.xml" );
            configurator.Configure();
        }

        public override void OnTransactionBegin( ITransactionState transaction )
        {
            Log.Debug( string.Format( "[{1}] Init Sessions {0}, {2}",
                    Interlocked.Increment( ref count ),
                    Thread.CurrentThread.ManagedThreadId,
                    ( HttpContext.Current == null ? "" : HttpContext.Current.Request.RawUrl )
                ) );

            SessionFactories.Instance.InitSessions();
        }

        public override void OnTransactionEnd( ITransactionState transaction )
        {
            Log.Debug( string.Format( "[{1}] Dispose Sessions {0}, {2}",
                    Interlocked.Decrement( ref count ),
                    Thread.CurrentThread.ManagedThreadId,
                    ( HttpContext.Current == null ? "" : HttpContext.Current.Request.RawUrl ) 
                ) );

            SessionFactories.Instance.DisposeSessions();
        }
    }
}