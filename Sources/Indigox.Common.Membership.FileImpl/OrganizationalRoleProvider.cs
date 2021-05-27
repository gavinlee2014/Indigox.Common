using System;
using System.Collections.Generic;
using Indigox.Common.Membership.FileImpl.Caches;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.FileImpl
{
    public class OrganizationalRoleProvider : IOrganizationalRoleProvider
    {
        public IOrganizationalRole GetOrganizationalRoleByID( string id )
        {
            if ( string.IsNullOrEmpty( id ) )
            {
                return null;
            }
            return (OrganizationalRole)MembersCache.GetPrincipalByTypeAndId( MembersCache.type_organizationalRole, id );
        }

        public IList<IOrganizationalRole> GetOrganizationalRoleByOrganizationalPerson( string personId )
        {
            List<IOrganizationalRole> organizationalRoles = new List<IOrganizationalRole>();
            foreach ( IContainer unit in MembersCache.GetAllUnits( personId ) )
            {
                if ( unit is IOrganizationalRole )
                {
                    organizationalRoles.Add( (IOrganizationalRole)unit );
                }
            }
            return organizationalRoles;
        }
    }
}