using System;
using System.Threading;
using Indigox.Common.StateContainer.CurrentUserProviders;
using NUnit.Framework;

namespace Indigox.Common.StateContainer.Test.Tests
{
    [TestFixture]
    public class SessionTest : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            MutableCurrentUserProvider.SetCurrentUser( "admin" );
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TestProperties()
        {
            ISessionState session = StateContext.Current.Session;
            session[ "testattr1" ] = 1;
            Assert.AreEqual( 1, session[ "testattr1" ] );
        }

        [Test]
        public void TestPropertiesCrossThread()
        {
            var thread = new Thread( new ParameterizedThreadStart( AddPropertyInAnotherThread ) );
            WaitForSubThread( thread );
            Assert.IsNotNull( StateContext.Current.Session[ "testattr2" ] );
        }

        private static void WaitForSubThread( Thread thread )
        {
            Console.WriteLine( "Wait for sub thread..." );
            thread.Start();
            Thread.Sleep( 100 );
            Console.WriteLine( "Main thread resumed." );
        }

        private static void AddPropertyInAnotherThread( object data )
        {
            Console.WriteLine( "Sub thread started." );
            StateContext.Current.Session[ "testattr2" ] = 2;
            Console.WriteLine( "Sub thread finished." );
        }
    }
}
