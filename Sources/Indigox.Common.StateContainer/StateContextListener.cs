using System;
using System.Collections.Generic;

namespace Indigox.Common.StateContainer
{
    public abstract class StateContextListener : IStateContextListener
    {
        public virtual void OnApplicationBegin( IApplicationState application )
        {
        }

        public virtual void OnApplicationEnd( IApplicationState application )
        {
        }

        public virtual void OnSessionBegin( ISessionState session )
        {
        }

        public virtual void OnSessionEnd( ISessionState session )
        {
        }

        public virtual void OnTransactionBegin( ITransactionState transaction )
        {
        }

        public virtual void OnTransactionEnd( ITransactionState transaction )
        {
        }
    }
}
