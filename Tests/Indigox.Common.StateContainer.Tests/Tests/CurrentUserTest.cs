using System;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.StateContainer.CurrentUserProviders;
using NUnit.Framework;

namespace Indigox.Common.StateContainer.Test.Tests
{
    [TestFixture]
    public class CurrentUserTest : TestBase
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
        public void TestCurrentUser()
        {
            ISessionState session = StateContext.Current.Session;
            IOrganizationalPerson user = session.User;
            Assert.IsNotNull( user );
            Console.WriteLine( "current user: " + user.AccountName );
        }
    }
}