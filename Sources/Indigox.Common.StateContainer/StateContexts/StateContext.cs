using System;
using System.Threading;
using Indigox.Common.Logging;
using Indigox.Common.StateContainer.States;

namespace Indigox.Common.StateContainer.StateContexts
{
    internal abstract class StateContext : IStateContext
    {
        public abstract IApplicationState Application
        {
            get;
            protected set;
        }

        public abstract ISessionState Session
        {
            get;
            protected set;
        }

        public abstract ITransactionState Transaction
        {
            get;
            protected set;
        }

        public void BeginApplication()
        {
            Log.Debug( string.Format( "Application begin on thread [{0}].", Thread.CurrentThread.ManagedThreadId ) );
            this.Application = new ApplicationState();
            foreach ( IStateContextListener listener in ListenerManager.Instance.Listeners )
            {
                try
                {
                    listener.OnApplicationBegin( this.Application );
                }
                catch ( Exception ex )
                {
                    ApplicationException warppedEx = new ApplicationException(
                        string.Format( "Invoke {0}.OnApplicationBegin failed.", listener.GetType().FullName ),
                        ex );
                    Log.Error( warppedEx.ToString() );
                    throw warppedEx;
                }
            }
        }

        public void EndApplication()
        {
            Log.Debug( string.Format( "Application end on thread [{0}].", Thread.CurrentThread.ManagedThreadId ) );
            foreach ( IStateContextListener listener in ListenerManager.Instance.Listeners )
            {
                try
                {
                    listener.OnApplicationEnd( this.Application );
                }
                catch ( Exception ex )
                {
                    ApplicationException warppedEx = new ApplicationException(
                        string.Format( "Invoke {0}.OnApplicationEnd failed.", listener.GetType().FullName ),
                        ex );
                    Log.Error( warppedEx.ToString() );
                    throw warppedEx;
                }
            }
            this.Application = null;
        }

        public void BeginSession( ICurrentUserProvider currentUserProvider )
        {
            Log.Debug( string.Format( "Session begin on thread [{0}].", Thread.CurrentThread.ManagedThreadId ) );
            this.Session = new SessionState( currentUserProvider );
            foreach ( IStateContextListener listener in ListenerManager.Instance.Listeners )
            {
                try
                {
                    listener.OnSessionBegin( this.Session );
                }
                catch ( Exception ex )
                {
                    ApplicationException warppedEx = new ApplicationException(
                        string.Format( "Invoke {0}.OnSessionBegin failed.", listener.GetType().FullName ),
                        ex );
                    Log.Error( warppedEx.ToString() );
                    throw warppedEx;
                }
            }
        }

        public void EndSession()
        {
            Log.Debug( string.Format( "Session end on thread [{0}].", Thread.CurrentThread.ManagedThreadId ) );
            foreach ( IStateContextListener listener in ListenerManager.Instance.Listeners )
            {
                try
                {
                    listener.OnSessionEnd( this.Session );
                }
                catch ( Exception ex )
                {
                    ApplicationException warppedEx = new ApplicationException(
                        string.Format( "Invoke {0}.OnSessionEnd failed.", listener.GetType().FullName ),
                        ex );
                    Log.Error( warppedEx.ToString() );
                    throw warppedEx;
                }
            }
            this.Session = null;
        }

        public void BeginTransaction()
        {
            Log.Debug( string.Format( "Transaction begin on thread [{0}].", Thread.CurrentThread.ManagedThreadId ) );
            this.Transaction = new TransactionState();
            foreach ( IStateContextListener listener in ListenerManager.Instance.Listeners )
            {
                try
                {
                    listener.OnTransactionBegin( this.Transaction );
                }
                catch ( Exception ex )
                {
                    ApplicationException warppedEx = new ApplicationException(
                        string.Format( "Invoke {0}.OnTransactionBegin failed.", listener.GetType().FullName ),
                        ex );
                    Log.Error( warppedEx.ToString() );
                    throw warppedEx;
                }
            }
        }

        public void EndTransaction()
        {
            Log.Debug( string.Format( "Transaction end on thread [{0}].", Thread.CurrentThread.ManagedThreadId ) );
            foreach ( IStateContextListener listener in ListenerManager.Instance.Listeners )
            {
                try
                {
                    listener.OnTransactionEnd( this.Transaction );
                }
                catch ( Exception ex )
                {
                    ApplicationException warppedEx = new ApplicationException(
                        string.Format( "Invoke {0}.OnTransactionEnd failed.", listener.GetType().FullName ),
                        ex );
                    Log.Error( warppedEx.ToString() );
                    throw warppedEx;
                }
            }
            this.Transaction = null;
        }
    }
}