using System;
using System.Collections.Generic;

namespace Indigox.Common.StateContainer.Test.ConfigurationTests
{
    public class DummyListener : IStateContextListener
    {
        public void OnApplicationBegin( IApplicationState application )
        {
            Console.WriteLine( "DummyListener: OnApplicationBegin." );
        }

        public void OnApplicationEnd( IApplicationState application )
        {
            Console.WriteLine( "DummyListener: OnApplicationEnd." );
        }

        public void OnSessionBegin( ISessionState session )
        {
            Console.WriteLine( "DummyListener: OnSessionBegin." );
        }

        public void OnSessionEnd( ISessionState session )
        {
            Console.WriteLine( "DummyListener: OnSessionEnd." );
        }

        public void OnTransactionBegin( ITransactionState transaction )
        {
            Console.WriteLine( "DummyListener: OnTransactionBegin." );
        }

        public void OnTransactionEnd( ITransactionState transaction )
        {
            Console.WriteLine( "DummyListener: OnTransactionEnd." );
        }
    }
}
