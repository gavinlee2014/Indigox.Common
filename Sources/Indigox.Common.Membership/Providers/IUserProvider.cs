using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Providers
{
    public interface IUserProvider
    {
        IUser GetUserByID( string id );

        IUser GetUserByAccount( string account );

        IList<IUser> GetAllUsers();
    }
}