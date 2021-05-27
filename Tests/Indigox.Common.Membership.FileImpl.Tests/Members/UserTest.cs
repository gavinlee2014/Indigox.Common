using System;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.FileImpl.NUnitTest.Utils;
using NUnit.Framework;

namespace Indigox.Common.Membership.FileImpl.NUnitTest.Members
{
    [TestFixture]
    internal class UserTest : FileImplTestFixture
    {
        [Test]
        [TestCase( "spfarmadmin", "OR1000000000" )]
        [TestCase( "admin", "OR1000000000" )]
        [TestCase( "hao", "OR1000000315" )]
        public void TestGetUserOrganization( string userToken, string expectedOrgToken )
        {
            IOrganizationalPerson user = PrincipalTestUtil.GetPrincipal<IOrganizationalPerson>( userToken );
            IOrganizationalUnit actualOrg = user.Organization;
            IOrganizationalUnit expectedOrg = PrincipalTestUtil.GetPrincipal<IOrganizationalUnit>( expectedOrgToken );
            Assert.AreEqual( expectedOrg, actualOrg );
        }
    }
}