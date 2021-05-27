using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using NUnit.Framework;

namespace Indigox.Common.Membership.NHibernateImpl.NUnitTest.Providers
{
    [TestFixture]
    public class NHBOrganizationalUnitProviderTest : NHibernateImplTestFixture
    {
        [Test]
        [TestCase( "OR1000000000" )]
        public void TestGetByID( string orgid )
        {
            IOrganizationalUnit org = OrganizationalUnit.GetOrganizationByID( orgid );
        }

        [Test]
        [TestCase( "OR1000000000" )]
        public void TestGetOrgAllUsers( string orgid )
        {
            IOrganizationalUnit org = OrganizationalUnit.GetOrganizationByID( orgid );
            IList<IOrganizationalPerson> users = org.GetAllUsers();
        }

        //[Test]
        public void TestHasMember( string orgid, string userid )
        {
            IOrganizationalUnit org = OrganizationalUnit.GetOrganizationByID( orgid );
            IOrganizationalPerson person = OrganizationalPerson.GetOrganizationalPersonByID( userid );
            Assert.True( org.ContainsMember( person ) );
        }
    }
}