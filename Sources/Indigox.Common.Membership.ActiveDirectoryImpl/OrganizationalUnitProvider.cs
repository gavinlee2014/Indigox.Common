using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.ActiveDirectoryImpl
{
    class OrganizationalUnitProvider : IOrganizationalUnitProvider
    {
        public IOrganizationalUnit GetOrganizationalUnitByID( string id )
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
