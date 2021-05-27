using System;
using Indigox.Common.Membership.Interfaces;
using NUnit.Framework;

namespace Indigox.Common.Membership.NHibernateImpl.NUnitTest.Providers
{
    [TestFixture]
    public class NHBOrganizationalPersonProviderTest : NHibernateImplTestFixture
    {
        [Test]
        [TestCase( "UR0000000001" )]
        public void TestGetUserByID( string userid )
        {
            IOrganizationalPerson user = OrganizationalPerson.GetOrganizationalPersonByID( userid );
            Assert.IsNotNull( user );
        }

        [Test]
        [TestCase( "UR0000000001" )]
        public void TestGetUserByIDReference( string userid )
        {
            IOrganizationalPerson user1 = OrganizationalPerson.GetOrganizationalPersonByID( userid );

            //NHibernateSessionFactory.ClearSession();
            // user2 is same as user1 when get it from session cache.
            // user2 is only equals to user1 when get it from second level cache.
            IOrganizationalPerson user2 = OrganizationalPerson.GetOrganizationalPersonByID( userid );
            Assert.AreEqual( user1, user2 );
            Assert.AreSame( user1, user2 );
        }

        [Test]
        [TestCase( "UR0000000001", "OR1000000000" )]
        [TestCase( "UR1000000095", "OR1000000087" )]
        [TestCase( "UR1000000099", "OR1000000074" )]
        public void TestGetUserOrganization( string userid, string orgid )
        {
            IOrganizationalPerson user = OrganizationalPerson.GetOrganizationalPersonByID( userid );
            IOrganizationalUnit org = user.Organization;
            Assert.AreEqual( orgid, org.ID );
        }
    }
}