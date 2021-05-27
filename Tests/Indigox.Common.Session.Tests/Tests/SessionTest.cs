using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Indigox.Common.Session.Test.Tests
{
    [TestFixture]
    public class SessionTest
    {
        [SetUp]
        public void SetUp()
        {
            //SessionManager.SetSession( new TestSession() );
        }

        [TearDown]
        public void TearDown()
        {
            SessionManager.AbandonCurrentSession();
        }

        [Test]
        public void TestProperties()
        {
            ISession session = SessionManager.GetCurrentSession();
            session.Properties.Add( "testattr1", 1 );
            Assert.AreEqual( 1, session.Properties[ "testattr1" ] );
        }

        [Test]
        public void TestPropertiesCrossThread()
        {
            var thread = new Thread( new ParameterizedThreadStart( AddPropertyInAnotherThread ) );
            WaitForSubThread( thread );
            Assert.IsFalse( SessionManager.GetCurrentSession().Properties.ContainsKey( "testattr2" ) );
        }

        private static void WaitForSubThread( Thread thread )
        {
            System.Diagnostics.Debug.WriteLine( "Wait for sub thread..." );
            thread.Start();
            Thread.Sleep( 100 );
            System.Diagnostics.Debug.WriteLine( "Main thread resumed." );
        }

        private static void AddPropertyInAnotherThread( object data )
        {
            System.Diagnostics.Debug.WriteLine( "Sub thread started." );
            SessionManager.GetCurrentSession().Properties.Add( "testattr2", 2 );
            System.Diagnostics.Debug.WriteLine( "Sub thread finished." );
        }
    }
}
