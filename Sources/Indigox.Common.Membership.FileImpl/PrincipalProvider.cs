using System;
using Indigox.Common.Membership.FileImpl.Caches;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.FileImpl
{
    public class PrincipalProvider : IPrincipalProvider
    {
        public IPrincipal GetPrincipalByID( string id )
        {
            if ( string.IsNullOrEmpty( id ) )
            {
                return null;
            }
            return MembersCache.GetPrincipalById( id );
        }
    }
}