using System;
using Indigox.Common.Membership.Interfaces;
using NUnit.Framework;

namespace Indigox.Common.Membership.NHibernateImpl.NUnitTest.Providers
{
    [TestFixture]
    internal class NHBContainerProviderTest : NHibernateImplTestFixture
    {
        [Test]
        [TestCase( "OR1000000071" )]
        public void TestGetContainerByID( string groupId )
        {
            IContainer group = Container.GetContainerByID( groupId );
            Assert.NotNull( group );
        }
    }
}
