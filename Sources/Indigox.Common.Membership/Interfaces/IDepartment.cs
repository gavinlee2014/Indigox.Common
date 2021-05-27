using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IDepartment : IOrganizationalUnit
    {
        IPrincipal Director { get; }
    }
}