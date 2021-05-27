using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutableDepartment : IDepartment, IMutableOrganizationalUnit
    {
        new IPrincipal Director { get; set; }
    }
}