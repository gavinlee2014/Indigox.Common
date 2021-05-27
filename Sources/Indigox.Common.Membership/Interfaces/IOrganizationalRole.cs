using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IOrganizationalRole : IContainer, IOrganizationalHolder
    {
        IRole Role { get; }
    }
}