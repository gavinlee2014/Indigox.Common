using System;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.StateContainer
{
    public interface ISessionState
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        IOrganizationalPerson User { get; }

        /// <summary>
        /// 获取或设置属性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[ string key ]
        {
            get;
            set;
        }
    }
}