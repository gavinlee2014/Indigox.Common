using System;
using System.Collections.Generic;
using Indigox.Common.Membership;

namespace Indigox.Common.Session.Common
{
    public abstract class AbstractSession : ISession
    {
        protected AbstractSession()
        {
            this.properties = new Dictionary<string, object>();
        }

        protected AbstractSession( StringComparer comparer )
        {
            this.properties = new Dictionary<string, object>( comparer );
        }

        private Dictionary<string, object> properties;
        private User currentUser;

        public virtual User CurrentUser
        {
            get
            {
                string username = null;
                TryGetCurrentUserAccountName( out username );
                if ( string.IsNullOrEmpty( username ) )
                {
                    return null;
                }
                if ( this.currentUser == null || this.currentUser.AccountName != username )
                {
                    User user = User.GetUserByAccount( username );
                    this.currentUser = user;
                }
                return this.currentUser;
            }
        }

        public Dictionary<string, object> Properties
        {
            get { return this.properties; }
        }

        /// <summary>
        /// 尝试获取当前登录用户的帐号，如果出错则取回 null
        /// </summary>
        /// <param name="username"></param>
        protected abstract void TryGetCurrentUserAccountName( out string username );
    }
}