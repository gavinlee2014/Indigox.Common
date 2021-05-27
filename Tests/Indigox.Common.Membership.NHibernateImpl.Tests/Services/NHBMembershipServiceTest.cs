using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.NHibernateImpl.NUnitTest.Utils;
using Indigox.Common.Membership.Services;
using NUnit.Framework;

namespace Indigox.Common.Membership.NHibernateImpl.NUnitTest.Service
{
    [TestFixture]
    public class NHBMembershipServiceTest : NHibernateImplTestFixture
    {
        [Test]
        [TestCase( "hao", "DP1000000402" )]
        [TestCase( "lcao", "DP1000000402" )]
        [TestCase( "yshi", "DP1000000447" )]
        public void TestGetOrganizationManager( string userToken, string expectedOrgManagerToken )
        {
            MembershipService service = new MembershipService();
            IOrganizationalPerson user = PrincipalTestUtil.GetPrincipal<IOrganizationalPerson>( userToken );
            IPrincipal actualOrgManager = service.GetOrganizationManager( user );
            IPrincipal expectedOrgManager = PrincipalTestUtil.GetPrincipal<IPrincipal>( expectedOrgManagerToken );
            Assert.IsNotNull( actualOrgManager );
            Assert.AreEqual( expectedOrgManager, actualOrgManager );
        }

        [Test]
        [TestCase( "hao", "DP1000000426" )]
        [TestCase( "lcao", "DP1000000426" )]
        public void TestGetOrganizationDirector( string userToken, string expectedOrgDirectorToken )
        {
            MembershipService service = new MembershipService();
            IOrganizationalPerson user = PrincipalTestUtil.GetPrincipal<IOrganizationalPerson>( userToken );
            IPrincipal actualOrgDirector = service.GetOrganizationDirector( user );
            IPrincipal expectedOrgDirector = PrincipalTestUtil.GetPrincipal<IPrincipal>( expectedOrgDirectorToken );
            Assert.IsNotNull( actualOrgDirector );
            Assert.AreEqual( expectedOrgDirector, actualOrgDirector );
        }

        [Test]
        [TestCase( 1, "hao", "DP1000000426" )]
        [TestCase( 1, "lcao", "DP1000000402" )]
        public void TestGetReportingManager( int reportingHierarchyId, string userToken, string expectedManagerToken )
        {
            MembershipService service = new MembershipService();
            IOrganizationalHolder user = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( userToken );
            IReportingHierarchy rh = ReportingHierarchy.GetReportingHierarchyByID( reportingHierarchyId );
            IOrganizationalHolder actualManager = service.GetReportingManager( user, rh );
            IOrganizationalHolder expectedManager = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( expectedManagerToken );
            Assert.IsNotNull( actualManager );
            Assert.AreEqual( expectedManager, actualManager );
        }

        [Test]
        [TestCase( "PS1000000928", "DP1000000865" )]
        public void TestInGroup( string unitToken, string principalToken )
        {
            IContainer unit = PrincipalTestUtil.GetPrincipal<IContainer>( unitToken );
            MembershipService service = new MembershipService();
            IPrincipal principal = PrincipalTestUtil.GetPrincipal<IPrincipal>( principalToken );
            Assert.IsTrue( service.InGroup( unitToken, principal ) );
        }

        [Test]
        [TestCase( "PS1000000928", "UR1000000747" )]
        [TestCase( "PS1000000928", "DP1000000865" )]
        public void TestInGroupEx( string unitToken, string principalToken )
        {
            IContainer unit = PrincipalTestUtil.GetPrincipal<IContainer>( unitToken );
            MembershipService service = new MembershipService();
            IPrincipal principal = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( principalToken );
            Assert.IsTrue( service.InGroupEx( unitToken, principal, true ) );
        }

        [Test]
        [TestCase( "UR1000000080", "DG1000000433;DP1000000402;DP1000000443;OR1000000000;OR1000000307;OR1000000315" )]
        public void TestGetUserAllGroups( string userToken, string expectedGroupsToken )
        {
            MembershipService service = new MembershipService();
            IUser user = PrincipalTestUtil.GetPrincipal<IUser>( userToken );
            IList<IContainer> actualGroups = service.GetUserAllGroups( user );
            IList<IContainer> expectedGroups = PrincipalTestUtil.GetPrincipals<IContainer>( expectedGroupsToken );
            PrincipalTestUtil.Sort<IContainer>( actualGroups );
            PrincipalTestUtil.Sort<IContainer>( expectedGroups );
            CollectionAssert.AreEqual( expectedGroups, actualGroups );
        }
    }
}