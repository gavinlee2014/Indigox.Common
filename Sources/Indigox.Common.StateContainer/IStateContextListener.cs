using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.StateContainer
{
    public interface IStateContextListener
    {
        void OnApplicationBegin( IApplicationState application );
        void OnApplicationEnd( IApplicationState application );
        void OnSessionBegin( ISessionState session );
        void OnSessionEnd( ISessionState session );
        void OnTransactionBegin( ITransactionState transaction );
        void OnTransactionEnd( ITransactionState transaction );
    }
}
