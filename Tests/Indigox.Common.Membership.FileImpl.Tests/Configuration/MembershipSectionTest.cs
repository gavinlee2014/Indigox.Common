using System;
using System.Configuration;
using Indigox.Common.Membership.Configuration;
using NUnit.Framework;

namespace Indigox.Common.Membership.FileImpl.NUnitTest.Configuration
{
    [TestFixture]
    public class MembershipSectionTest : FileImplTestFixture
    {
        [TestFixtureSetUp]
        public void SetUpFixture()
        {
        }

        private static readonly Type ProviderProviderType = typeof( Indigox.Common.Membership.FileImpl.ProviderFactory );

        [Test]
        public void TestGetSection()
        {
            MembershipSection section = ConfigurationManager.GetSection( "indigo/membership" ) as MembershipSection;
            Assert.AreEqual( ProviderProviderType, Type.GetType( section.ProviderFactory.ProviderFactoryType ) );
        }

        [Test]
        public void TestDefaultSection()
        {
            MembershipSection section = MembershipSection.Default;
            Assert.AreEqual( ProviderProviderType, Type.GetType( section.ProviderFactory.ProviderFactoryType ) );
        }
    }
}