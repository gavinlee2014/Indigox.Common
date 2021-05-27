using System;
using Indigox.Common.Membership.FileImpl.Caches;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.FileImpl
{
    public class GroupProvider : IGroupProvider
    {
        public IGroup GetGroupByID( string id )
        {
            if ( string.IsNullOrEmpty( id ) )
            {
                return null;
            }
            return (Group)MembersCache.GetPrincipalByTypeAndId( MembersCache.type_group, id );
        }
    }
}