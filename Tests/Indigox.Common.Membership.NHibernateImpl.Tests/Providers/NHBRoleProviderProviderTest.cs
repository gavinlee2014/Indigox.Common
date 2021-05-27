using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;
using NUnit.Framework;

namespace Indigox.Common.Membership.NHibernateImpl.NUnitTest.Providers
{
    [TestFixture]
    public class NHBRoleProviderProviderTest : NHibernateImplTestFixture
    {
        [Test]
        [TestCase( "PS1000000046" )]
        [TestCase( "PS1000000046&1" )]
        public void TestGetRoleByID( string id )
        {
            IRole role = Role.GetRoleByID( id );
            Assert.IsNotNull( role );
            Assert.AreEqual( id, role.ID );
        }

        [Test]
        [TestCase( "PS1000000928&1", "UR1000000264", "DP1000000879" )]
        [TestCase( "PS1000000524&1", "DP1000000522", "DP1000000525" )]
        public void TestGetOrganizationalRoleFromRole( string relativePositionId, string holderId, string roleId )
        {
            IOrganizationalHolder holder = (IOrganizationalHolder)Principal.GetPrincipalByID( holderId );
            IRole role = Role.GetRoleByID( relativePositionId );
            IList<IOrganizationalRole> organizationalRoles = ProviderFactories.GetFactory().GetRoleProvider().GetOrganizationalRoleFromRole( holder, role );
            Assert.IsNotNull( organizationalRoles );

            //Assert.AreEqual( 1, roles.Count );
            //Assert.AreEqual( roleId, roles[ 0 ].ID );
        }
    }
}