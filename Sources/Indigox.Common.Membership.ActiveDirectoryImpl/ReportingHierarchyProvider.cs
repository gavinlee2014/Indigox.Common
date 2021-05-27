using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.ActiveDirectoryImpl
{
    class ReportingHierarchyProvider : IReportingHierarchyProvider
    {
        public IOrganizationalHolder GetManagerCrossLevel( IReportingHierarchy hierarchy, IOrganizationalHolder role, int level )
        {
            throw new NotImplementedException();
        }

        public IList<IOrganizationalHolder> GetDirectReporters( IReportingHierarchy hierarchy, IOrganizationalHolder role )
        {
            throw new NotImplementedException();
        }

        public IList<IOrganizationalHolder> GetReportingLine( IReportingHierarchy hierarchy, IOrganizationalHolder role )
        {
            throw new NotImplementedException();
        }

        public void SetManager( IReportingHierarchy hierarchy, IOrganizationalHolder role, IOrganizationalHolder manager )
        {
            throw new NotImplementedException();
        }

        public IReportingHierarchy GetReportingHierarchyById( int id )
        {
            throw new NotImplementedException();
        }

        public IList<IReportingHierarchy> GetAllReportingHierarchy()
        {
            throw new NotImplementedException();
        }

        public void AddReportingHierarchy( IReportingHierarchy hierarchy )
        {
            throw new NotImplementedException();
        }

        public void UpdateReportingHierarchy( IReportingHierarchy hierarchy )
        {
            throw new NotImplementedException();
        }

        public void RemoveReportingHierarchy( IReportingHierarchy hierarchy )
        {
            throw new NotImplementedException();
        }
    }
}
