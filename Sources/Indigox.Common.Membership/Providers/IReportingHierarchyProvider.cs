using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Providers
{
    public interface IReportingHierarchyProvider
    {
        /// <summary>
        /// 获取用户的第n层上级
        /// </summary>
        IOrganizationalHolder GetManagerCrossLevel( IReportingHierarchy hierarchy, IOrganizationalHolder role, int level );

        /// <summary>
        /// 获取直接向用户汇报的所有人
        /// </summary>
        IList<IOrganizationalHolder> GetDirectReporters( IReportingHierarchy hierarchy, IOrganizationalHolder role );

        /// <summary>
        /// 获取用户的汇报关系链（从用户到他的最高级领导）
        /// </summary>
        IList<IOrganizationalHolder> GetReportingLine( IReportingHierarchy hierarchy, IOrganizationalHolder role );

        /// <summary>
        /// 设置用户的直接上级
        /// </summary>
        void SetManager( IReportingHierarchy hierarchy, IOrganizationalHolder role, IOrganizationalHolder manager );

        IReportingHierarchy GetReportingHierarchyById( int id );

        IList<IReportingHierarchy> GetAllReportingHierarchy();

        void AddReportingHierarchy( IReportingHierarchy hierarchy );

        void UpdateReportingHierarchy( IReportingHierarchy hierarchy );

        void RemoveReportingHierarchy( IReportingHierarchy hierarchy );
    }
}