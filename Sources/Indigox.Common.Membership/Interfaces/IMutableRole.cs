using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutableRole : IRole, IMutableContainer
    {
        new RoleLevel Level { get; set; }
    }
}