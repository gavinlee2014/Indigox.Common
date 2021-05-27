using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutableOrganizationalHolder : IOrganizationalHolder, IMutablePrincipal, IMutableOrganizationalObject
    {
    }
}