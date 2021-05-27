using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.ActiveDirectoryImpl
{
    class PrincipalProvider : IPrincipalProvider
    {
        public IPrincipal GetPrincipalByID( string id )
        {
            return new UserProvider().GetUserByID( id );
        }
    }
}
