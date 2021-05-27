using System;
using System.Collections.Generic;
using Indigox.Common.Membership;

namespace Indigox.Common.StateContainer.StateContainers
{
    public class SessionState : ISessionState
    {
        public SessionState()
        {
            this.properties = new Dictionary<string, object>();
        }

        public SessionState( ICurrentUserProvider currentUserProvider )
        {
            this.currentUserProvider = currentUserProvider;
            this.properties = new Dictionary<string, object>();
        }

        private Dictionary<string, object> properties;
        private User currentUser;
        private ICurrentUserProvider currentUserProvider;

        public virtual User User
        {
            get
            {
                string username = currentUserProvider.GetCurrentUser();
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
    }
}