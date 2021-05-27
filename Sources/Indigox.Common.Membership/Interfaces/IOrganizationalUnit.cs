using System;
using System.Collections.Generic;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IOrganizationalUnit : IContainer, IOrganizationalObject
    {
        IPrincipal Manager { get; }

        new IList<IOrganizationalObject> GetAllMembers();

        new IList<IOrganizationalPerson> GetAllUsers();

        /// <summary>
        /// 获取部门经理
        /// </summary>
        IPrincipal GetManager();

        /// <summary>
        /// 获取部门负责人
        /// </summary>
        IPrincipal GetDirector();
    }
}