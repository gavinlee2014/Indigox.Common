using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using NUnit.Framework;

namespace Indigox.Common.Membership.FileImpl.NUnitTest.Members
{
    [TestFixture]
    public class GroupTest : FileImplTestFixture
    {
        [Test]
        public void TestGetAllUserMembers()
        {
            IContainer unit = OrganizationalUnit.GetOrganizationByID( "OR1000000000" );
            IList<IUser> users = unit.GetAllUsers();
        }
    }
}