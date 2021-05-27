using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutableOrganizationalRole : IOrganizationalRole, IMutableContainer, IMutableOrganizationalHolder
    {
        new IRole Role { get; set; }
    }
}