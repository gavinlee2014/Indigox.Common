using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IOrganizationalObject
    {
        IOrganizationalUnit Organization { get; }
    }
}