using System;
using System.Collections.Generic;
using Indigox.Common.Membership.FileImpl.Caches;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.FileImpl
{
    public class UserProvider : IUserProvider
    {
        public IUser GetUserByID( string id )
        {
            if ( string.IsNullOrEmpty( id ) )
            {
                return null;
            }
            return MembersCache.GetUserById( id );
        }

        public IUser GetUserByAccount( string account )
        {
            if ( string.IsNullOrEmpty( account ) )
            {
                return null;
            }
            return MembersCache.GetUserByAccount( account );
        }

        public IList<IUser> GetAllUsers()
        {
            return MembersCache.GetAllUsers();
        }
    }
}