using System;
using Indigox.Common.Membership.FileImpl;
using Indigox.Common.Membership.Providers;
using NUnit.Framework;

namespace Indigox.Common.Membership.FileImpl.NUnitTest
{
    [Category( "FileImplTests" )]
    public class FileImplTestFixture
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            AssertIsTestProvider();
        }

        private void AssertIsTestProvider()
        {
            Assert.IsInstanceOf<UserProvider>( ProviderFactories.GetFactory().GetUserProvider() );
            Assert.IsInstanceOf<GroupProvider>( ProviderFactories.GetFactory().GetGroupProvider() );
            Assert.IsInstanceOf<OrganizationalRoleProvider>( ProviderFactories.GetFactory().GetOrganizationalRoleProvider() );
            Assert.IsInstanceOf<OrganizationalUnitProvider>( ProviderFactories.GetFactory().GetOrganizationalUnitProvider() );
            Assert.IsInstanceOf<ReportingHierarchyProvider>( ProviderFactories.GetFactory().GetReportingHierarchyProvider() );
        }
    }
}