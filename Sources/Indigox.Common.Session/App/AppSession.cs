using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Membership;
using Indigox.Common.Session.Common;

namespace Indigox.Common.Session.App
{
    public class AppSession : AbstractSession
    {
        /// <summary>
        /// 尝试获取当前登录用户的帐号，如果出错则取回 null
        /// </summary>
        /// <param name="username"></param>
        protected override void TryGetCurrentUserAccountName( out string username )
        {
            try
            {
                username = Environment.UserName;
            }
            catch ( Exception )
            {
                username = null;
            }
        }
    }
}