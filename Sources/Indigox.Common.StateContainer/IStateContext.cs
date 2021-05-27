using System;
using Indigox.Common.StateContainer.CurrentUserProviders;

namespace Indigox.Common.StateContainer
{
    public interface IStateContext
    {
        IApplicationState Application { get; }
        ISessionState Session { get; }
        ITransactionState Transaction { get; }

        void BeginApplication();
        void EndApplication();
        void BeginSession( ICurrentUserProvider currentUserProvider );
        void EndSession();
        void BeginTransaction();
        void EndTransaction();
    }
}