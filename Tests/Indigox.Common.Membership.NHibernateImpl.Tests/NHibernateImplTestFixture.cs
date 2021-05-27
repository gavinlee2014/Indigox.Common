using System;
using Indigox.Common.Membership.NHibernateImpl.NUnitTest.TestFixtureProxies;
using Indigox.TestUtility.TestFixtures;
using NUnit.Framework;

namespace Indigox.Common.Membership.NHibernateImpl.NUnitTest
{
    [Category( "NHibernateImplTests" )]
    [TestFixtureProxy(
        typeof( StateContextTestFixtureProxy ),
        typeof( NHibernateRepositoryTestFixtureProxy )
    )]
    public class NHibernateImplTestFixture : BaseTestFixture
    {
    }
}