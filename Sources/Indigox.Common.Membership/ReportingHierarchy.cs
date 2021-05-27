using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class ReportingHierarchy : IReportingHierarchy
    {
        public static IReportingHierarchy GetReportingHierarchyByID( int id )
        {
            return ProviderFactories.GetFactory().GetReportingHierarchyProvider().GetReportingHierarchyById( id );
        }

        private int id;
        private string name;

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public IOrganizationalHolder GetManager( IOrganizationalHolder role )
        {
            var rhp = ProviderFactories.GetFactory().GetReportingHierarchyProvider();
            return rhp.GetManagerCrossLevel( this, role, 1 );
        }

        public IOrganizationalHolder GetManagerCrossLevel( IOrganizationalHolder role, int level )
        {
            var rhp = ProviderFactories.GetFactory().GetReportingHierarchyProvider();
            return rhp.GetManagerCrossLevel( this, role, level );
        }

        public IList<IOrganizationalHolder> GetDirectReporters( IOrganizationalHolder role )
        {
            var rhp = ProviderFactories.GetFactory().GetReportingHierarchyProvider();
            return rhp.GetDirectReporters( this, role );
        }

        public IList<IOrganizationalHolder> GetReportingLine( IOrganizationalHolder role )
        {
            var rhp = ProviderFactories.GetFactory().GetReportingHierarchyProvider();
            return rhp.GetReportingLine( this, role );
        }

        public void SetManager( IOrganizationalHolder role, IOrganizationalHolder manager )
        {
            IList<IOrganizationalHolder> reportingLine = GetReportingLine( manager );
            foreach ( IOrganizationalHolder managerManager in reportingLine )
            {
                if ( managerManager.ID == role.ID )
                {
                    throw new Exception(
                        string.Format( "[{0}]是[{1}]的上级，不能再将其经理设置为[{1}]。",
                                       role.Name, manager.Name ) );
                }
            }
            var rhp = ProviderFactories.GetFactory().GetReportingHierarchyProvider();
            rhp.SetManager( this, role, manager );
        }
    }
}