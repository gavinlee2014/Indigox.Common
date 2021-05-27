using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Indigox.Common.Membership;
using Indigox.Common.Session.Common;

namespace Indigox.Common.Session.Web
{
    public class HttpContextSession : AbstractSession
    {
        /// <summary>
        /// 尝试获取当前登录用户的帐号，如果出错则取回 null
        /// </summary>
        /// <param name="username"></param>
        protected override void TryGetCurrentUserAccountName( out string username )
        {
            try
            {
                username = HttpContext.Current.User.Identity.Name;
                if ( username.Contains( "\\" ) )
                {
                    username = username.Substring( username.IndexOf( "\\" ) + 1 );
                }
            }
            catch ( Exception )
            {
                username = null;
            }
        }
    }
}