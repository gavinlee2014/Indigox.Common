using System;
using Indigox.Common.Membership.FileImpl.Caches;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.FileImpl
{
    public class OrganizationalUnitProvider : IOrganizationalUnitProvider
    {
        public IOrganizationalUnit GetOrganizationalUnitByID( string id )
        {
            if ( string.IsNullOrEmpty( id ) )
            {
                return null;
            }
            return (OrganizationalUnit)MembersCache.GetPrincipalByTypeAndId( MembersCache.type_organizationalUnit, id );
        }

        public IPrincipal GetManager( IOrganizationalUnit organization )
        {
            throw new NotImplementedException();
        }

        public IPrincipal GetDirector( IOrganizationalUnit organization )
        {
            throw new NotImplementedException();
        }

        public ICorporation GetCorporation()
        {
            return GetOrganizationalUnitByID( "OR1000000000" ) as ICorporation;
        }
    }
}