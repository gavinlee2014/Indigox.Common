using System;
using System.Collections.Generic;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IContainer : IPrincipal
    {
        IPrincipal Owner { get; }

        // inverse = true
        // it means must update all member after members array changed
        IList<IPrincipal> Members { get; }

        bool ContainsMember( IPrincipal member );

        IList<IPrincipal> GetAllMembers();

        IList<IUser> GetAllUsers();
    }
}