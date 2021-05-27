using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Indigox.Common.Membership;

namespace Indigox.Common.Session
{
    public interface ISession
    {

        /// <summary>
        /// 获取当前用户
        /// </summary>
        User CurrentUser { get; }

        /// <summary>
        /// 获取保存在 Session 上的属性
        /// </summary>
        Dictionary<string, object> Properties { get; }
    }
}
