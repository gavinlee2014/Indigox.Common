using System;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Providers
{
    public interface IOrganizationalUnitProvider
    {
        /// <summary>
        /// 根据ID获取部门对象
        /// </summary>
        IOrganizationalUnit GetOrganizationalUnitByID( string id );

        /// <summary>
        /// 获取部门经理
        /// </summary>
        IPrincipal GetManager( IOrganizationalUnit organization );

        /// <summary>
        /// 获取部门负责人
        /// </summary>
        IPrincipal GetDirector( IOrganizationalUnit organization );

        /// <summary>
        /// 获取集团根节点
        /// </summary>
        ICorporation GetCorporation();
    }
}