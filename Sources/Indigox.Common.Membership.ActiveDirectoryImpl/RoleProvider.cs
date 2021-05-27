using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.ActiveDirectoryImpl
{
    class RoleProvider : IRoleProvider
    {
        public IRole GetRoleByID( string id )
        {
            throw new NotImplementedException();
        }

        public IList<IOrganizationalRole> GetOrganizationalRoleFromRole( IOrganizationalHolder holder, IRole role )
        {
            throw new NotImplementedException();
        }
    }
}
