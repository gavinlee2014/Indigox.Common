using System;
using System.Collections.Generic;
using Indigox.Common.Membership.FileImpl.Caches;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.FileImpl
{
    public class ReportingHierarchyProvider : IReportingHierarchyProvider
    {
        public IOrganizationalHolder GetManagerCrossLevel( IReportingHierarchy hierarchy, IOrganizationalHolder holder, int level )
        {
            if ( level < 1 )
            {
                throw new ArgumentException( "level must greater or equal to 1." );
            }
            Dictionary<string, string> nodes = ReportingHierarchyCache.GetNodes( hierarchy );
            string managerId = holder.ID;
            for ( int i = level; i >= 1; i-- )
            {
                if ( nodes.ContainsKey( managerId ) )
                {
                    managerId = nodes[ managerId ];
                    //Log.Debug( "manager id : " + managerId );
                }
                else
                {
                    return null;
                }
            }
            return (IOrganizationalHolder)Principal.GetPrincipalByID( managerId );
        }

        public IList<IOrganizationalHolder> GetDirectReporters( IReportingHierarchy hierarchy, IOrganizationalHolder holder )
        {
            Dictionary<string, string> nodes = ReportingHierarchyCache.GetNodes( hierarchy );
            List<IOrganizationalHolder> directReporters = new List<IOrganizationalHolder>();
            foreach ( KeyValuePair<string, string> item in nodes )
            {
                if ( item.Value == holder.ID )
                {
                    directReporters.Add( (IOrganizationalHolder)Principal.GetPrincipalByID( item.Key ) );
                }
            }
            return directReporters;
        }

        public IList<IOrganizationalHolder> GetReportingLine( IReportingHierarchy hierarchy, IOrganizationalHolder holder )
        {
            Dictionary<string, string> nodes = ReportingHierarchyCache.GetNodes( hierarchy );
            List<IOrganizationalHolder> directReporters = new List<IOrganizationalHolder>();
            directReporters.Add( holder );
            IOrganizationalHolder tempUser = holder;
            while ( nodes.ContainsKey( tempUser.ID ) )
            {
                string managerId = nodes[ tempUser.ID ];
                tempUser = (IOrganizationalHolder)Principal.GetPrincipalByID( managerId );
                directReporters.Add( tempUser );
            }
            return directReporters;
        }

        public void SetManager( IReportingHierarchy hierarchy, IOrganizationalHolder holder, IOrganizationalHolder manager )
        {
            Dictionary<string, string> nodes = ReportingHierarchyCache.GetNodes( hierarchy );
            if ( nodes.ContainsKey( holder.ID ) )
            {
                nodes[ holder.ID ] = manager.ID;
            }
            else
            {
                nodes.Add( holder.ID, manager.ID );
            }
        }

        public IReportingHierarchy GetReportingHierarchyById( int id )
        {
            return ReportingHierarchyCache.GetReportingHierarchyById( id );
        }

        public IList<IReportingHierarchy> GetAllReportingHierarchy()
        {
            return ReportingHierarchyCache.GetAllReportingHierarchy();
        }

        public void AddReportingHierarchy( IReportingHierarchy hierarchy )
        {
        }

        public void UpdateReportingHierarchy( IReportingHierarchy hierarchy )
        {
        }

        public void RemoveReportingHierarchy( IReportingHierarchy hierarchy )
        {
        }
    }
}