using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Membership;
using Indigox.Common.Session;
using Indigox.Common.Session.Common;
using NUnit.Framework;

namespace Indigox.Common.Session.Test.Tests
{
    [TestFixture]
    public class SessionManagerTest
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
        public void TestCurrentUser()
        {
            ISession session = SessionManager.GetCurrentSession();
            User user = session.CurrentUser;
            Assert.IsNotNull( user );
            System.Diagnostics.Debug.WriteLine( "current user: " + user.AccountName );
        }

        [Test]
        public void TestSetSession()
        {
            ISession session = new MutableSession();
            SessionManager.SetCurrentSession( session );
            Assert.AreSame( session, SessionManager.GetCurrentSession() );
        }
    }
}