using System;
using System.Collections.Generic;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IReportingHierarchy
    {
        /// <summary>
        /// 汇报关系树编号
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// 汇报关系树名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取用户的上级
        /// </summary>
        /// <param name="role">用户</param>
        /// <returns></returns>
        IOrganizationalHolder GetManager( IOrganizationalHolder role );

        /// <summary>
        /// 获取用户的第n层上级
        /// </summary>
        /// <param name="role">用户</param>
        /// <param name="level">第n层</param>
        /// <returns></returns>
        IOrganizationalHolder GetManagerCrossLevel( IOrganizationalHolder role, int level );

        /// <summary>
        /// 获取直接向用户汇报的所有人
        /// </summary>
        /// <param name="role">用户</param>
        /// <returns></returns>
        IList<IOrganizationalHolder> GetDirectReporters( IOrganizationalHolder role );

        /// <summary>
        /// 获取用户的汇报关系链（从用户到他的最高级领导）
        /// </summary>
        /// <param name="role">用户</param>
        /// <returns></returns>
        IList<IOrganizationalHolder> GetReportingLine( IOrganizationalHolder role );

        /// <summary>
        /// 设置用户的直接上级
        /// </summary>
        /// <param name="role">用户</param>
        /// <param name="manager"></param>
        void SetManager( IOrganizationalHolder role, IOrganizationalHolder manager );
    }
}