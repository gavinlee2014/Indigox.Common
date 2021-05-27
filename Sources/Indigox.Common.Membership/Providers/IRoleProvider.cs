using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Providers
{
    public interface IRoleProvider
    {
        IRole GetRoleByID( string id );

        IList<IOrganizationalRole> GetOrganizationalRoleFromRole( IOrganizationalHolder holder, IRole role );
    }
}