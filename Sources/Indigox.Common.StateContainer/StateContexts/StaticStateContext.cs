using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.StateContainer.StateContexts
{
    /// <summary>
    /// Bind session context to thread.
    /// </summary>
    internal class StaticStateContext : StateContext
    {
        private static IApplicationState application;
        private static ISessionState session;
        private static ITransactionState transaction;

        public override IApplicationState Application
        {
            get
            {
                return application;
            }
            protected set
            {
                application = value;
            }
        }

        public override ISessionState Session
        {
            get
            {
                return session;
            }
            protected set
            {
                session = value;
            }
        }

        public override ITransactionState Transaction
        {
            get
            {
                return transaction;
            }
            protected set
            {
                transaction = value;
            }
        }
    }
}