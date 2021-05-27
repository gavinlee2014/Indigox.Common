using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Providers
{
    public interface IOrganizationalRoleProvider
    {
        IOrganizationalRole GetOrganizationalRoleByID( string id );

        IList<IOrganizationalRole> GetOrganizationalRoleByOrganizationalPerson( string personId );
    }
}