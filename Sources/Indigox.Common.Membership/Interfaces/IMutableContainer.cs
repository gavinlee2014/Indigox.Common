using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutableContainer : IContainer, IMutablePrincipal
    {
        new IPrincipal Owner { get; set; }

        void AddMember( IPrincipal member );

        void RemoveMember( IPrincipal member );

        void ClearMembers();
    }
}