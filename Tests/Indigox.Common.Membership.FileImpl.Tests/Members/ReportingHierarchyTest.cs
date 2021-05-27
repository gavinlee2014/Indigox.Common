using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.FileImpl.NUnitTest.Utils;
using NUnit.Framework;

namespace Indigox.Common.Membership.FileImpl.NUnitTest.Members
{
    [TestFixture]
    public class ReportingHierarchyTest : FileImplTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            reportingHierarchy = ReportingHierarchy.GetReportingHierarchyByID( 1 );
        }

        [TearDown]
        public void TearDown()
        {
        }

        private IReportingHierarchy reportingHierarchy;

        [Test]
        [TestCase( "hao", "DP1000000426" )]
        [TestCase( "lcao", "DP1000000402" )]
        [TestCase( "yshi", "DP1000000447" )]
        [TestCase( "admin", null )]
        public void TestGetManager( string userToken, string expectedManagerToken )
        {
            IOrganizationalHolder user = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( userToken );
            IOrganizationalHolder expectedManager = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( expectedManagerToken );
            IOrganizationalHolder actualManager = (IOrganizationalHolder)reportingHierarchy.GetManager( user );
            Assert.AreEqual( expectedManager, actualManager );
        }

        [Test]
        [TestCase( "hao", 1, "DP1000000426" )]
        [TestCase( "kxli", 2, null )]
        [TestCase( "lcao", 1, "DP1000000402" )]
        [TestCase( "yshi", 1, "DP1000000447" )]
        [TestCase( "hao", 3, "kxli" )]
        [TestCase( "hao", 4, null )]
        public void TestGetManagerCrossLevel( string userToken, int level, string expectedManagerToken )
        {
            IOrganizationalHolder user = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( userToken );
            IOrganizationalHolder expectedManager = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( expectedManagerToken );
            IOrganizationalHolder actualManager = (IOrganizationalHolder)reportingHierarchy.GetManagerCrossLevel( user, level );
            Assert.AreEqual( expectedManager, actualManager );
        }

        [Test]
        [TestCase( "lcao", "" )]
        [TestCase( "DP1000000402", "DP1000000443;DP1000000444;lcao;jyang;yfxue" )]
        [TestCase( "DP1000000426", "DP1000000402;hao" )]
        public void TestGetDirectReporters( string userToken, string expectedDirectReporterTokens )
        {
            IOrganizationalHolder user = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( userToken );
            IList<IOrganizationalHolder> actualDirectReporters = reportingHierarchy.GetDirectReporters( user );
            IList<IOrganizationalHolder> expectedDirectReporters = PrincipalTestUtil.GetPrincipals<IOrganizationalHolder>( expectedDirectReporterTokens );
            PrincipalTestUtil.Sort<IOrganizationalHolder>( actualDirectReporters );
            PrincipalTestUtil.Sort<IOrganizationalHolder>( expectedDirectReporters );
            CollectionAssert.AreEqual( expectedDirectReporters, actualDirectReporters );
        }

        //[Test]
        //[TestCase( "lcao", "hao" )]
        //public void TestSetManager( string userID, string managerID )
        //{
        //    IOrganizationalHolder user = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( userID );
        //    IOrganizationalHolder manager = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( managerID );
        //    reportingHierarchy.SetManager( user, manager );
        //
        //    IOrganizationalHolder gettedManager = (IOrganizationalHolder)reportingHierarchy.GetManager( OrganizationalPerson.GetUserByAccount( userID ) );
        //    Assert.AreEqual( manager, gettedManager );
        //}

        //[Test]
        //[TestCase( "DP1000000426", "hao" )]
        //public void TestSetReporterAsManager( string userID, string managerID )
        //{
        //    IOrganizationalHolder user = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( userID );
        //    IOrganizationalHolder manager = PrincipalTestUtil.GetPrincipal<IOrganizationalHolder>( managerID );
        //    try
        //    {
        //        reportingHierarchy.SetManager( user, manager );
        //    }
        //    catch ( Exception ex )
        //    {
        //        Console.WriteLine( ex.Message );
        //        Assert.Pass();
        //    }
        //    Assert.Fail( "Expect exception." );
        //}
    }
}