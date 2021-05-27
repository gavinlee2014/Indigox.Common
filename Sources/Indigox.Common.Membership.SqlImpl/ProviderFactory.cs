using System;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.SqlImpl
{
    public class ProviderFactory : IProviderFactory
    {
        public IUserProvider GetUserProvider()
        {
            return new UserProvider();
        }

        public IGroupProvider GetGroupProvider()
        {
            throw new NotImplementedException();
        }

        public IReportingHierarchyProvider GetReportingHierarchyProvider()
        {
            throw new NotImplementedException();
        }

        public IPrincipalProvider GetPrincipalProvider()
        {
            throw new NotImplementedException();
        }

        public IOrganizationalUnitProvider GetOrganizationalUnitProvider()
        {
            throw new NotImplementedException();
        }

        public IOrganizationalRoleProvider GetOrganizationalRoleProvider()
        {
            throw new NotImplementedException();
        }

        public IRoleProvider GetRoleProvider()
        {
            throw new NotImplementedException();
        }
    }
}
