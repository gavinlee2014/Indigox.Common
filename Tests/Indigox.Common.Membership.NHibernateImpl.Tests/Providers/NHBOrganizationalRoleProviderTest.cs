using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using NUnit.Framework;

namespace Indigox.Common.Membership.NHibernateImpl.NUnitTest.Providers
{
    [TestFixture]
    public class NHBOrganizationalRoleProviderTest : NHibernateImplTestFixture
    {
        [Test]
        [TestCase( "UR1000000079" )]
        public void TestGetOrganizationalRoleByOrganizationalPerson( string personId )
        {
            IList<IOrganizationalRole> roles = OrganizationalRole.GetOrganizationalRoleByOrganizationalPerson( personId );
            foreach ( IOrganizationalRole role in roles )
            {
                Console.WriteLine( role.ID );
            }
            Assert.NotNull( roles );
        }
    }
}