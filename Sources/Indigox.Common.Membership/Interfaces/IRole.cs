using System;
using System.Collections.Generic;
using Indigox.Common.Utilities;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IRole : IContainer
    {
        RoleLevel Level { get; }

        /// <summary>
        /// 获取用户的绝对角色
        /// </summary>
        IList<IOrganizationalRole> GetOrganizationalRoles( IOrganizationalHolder holder );
    }

    public enum RoleLevel
    {
        [EnumValueDescription( "集团" )]
        Corporation = 101,

        [EnumValueDescription( "公司" )]
        Company = 102,

        [EnumValueDescription( "一级部门" )]
        Department = 103,

        [EnumValueDescription( "二级部门" )]
        Section = 104,
    }
}