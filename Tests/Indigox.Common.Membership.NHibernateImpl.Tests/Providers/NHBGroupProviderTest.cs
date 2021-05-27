using System;
using Indigox.Common.Membership.Interfaces;
using NUnit.Framework;

namespace Indigox.Common.Membership.NHibernateImpl.NUnitTest.Providers
{
    //[TestFixture]
    public class NHBGroupProviderTest : NHibernateImplTestFixture
    {
        //[Test]
        //[TestCase( "OR1000000331" )]
        public void TestGetGroupByID( string groupId )
        {
            IGroup group = Group.GetGroupByID( groupId );
            Assert.NotNull( group );
        }
    }
}