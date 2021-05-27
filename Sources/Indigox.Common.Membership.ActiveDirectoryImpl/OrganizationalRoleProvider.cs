using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.ActiveDirectoryImpl
{
    class OrganizationalRoleProvider : IOrganizationalRoleProvider
    {
        public IOrganizationalRole GetOrganizationalRoleByID( string id )
        {
            throw new NotImplementedException();
        }

        public IList<IOrganizationalRole> GetOrganizationalRoleByOrganizationalPerson( string personId )
        {
            throw new NotImplementedException();
        }
    }
}
