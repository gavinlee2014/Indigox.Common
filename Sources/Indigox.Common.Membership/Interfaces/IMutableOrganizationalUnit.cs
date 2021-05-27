using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutableOrganizationalUnit : IOrganizationalUnit, IMutableContainer, IMutableOrganizationalObject
    {
        new IPrincipal Manager { get; set; }
    }
}