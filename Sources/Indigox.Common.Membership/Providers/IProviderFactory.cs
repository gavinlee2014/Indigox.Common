using System;

namespace Indigox.Common.Membership.Providers
{
    public interface IProviderFactory
    {
        IPrincipalProvider GetPrincipalProvider();

        IUserProvider GetUserProvider();

        IOrganizationalUnitProvider GetOrganizationalUnitProvider();

        IOrganizationalRoleProvider GetOrganizationalRoleProvider();

        IRoleProvider GetRoleProvider();

        IGroupProvider GetGroupProvider();

        IReportingHierarchyProvider GetReportingHierarchyProvider();
    }
}