using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutableOrganizationalObject : IOrganizationalObject
    {
        new IOrganizationalUnit Organization { get; set; }
    }
}