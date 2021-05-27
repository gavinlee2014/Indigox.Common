using System.Collections.Generic;
using Indigox.Common.Membership;

namespace Indigox.Common.Session
{
    public class ImpersonateSession : ISession
    {
        private User currentUser;
        private Dictionary<string, object> properties = new Dictionary<string, object>();

        public User CurrentUser
        {
            get { return this.currentUser; }
        }

        public Dictionary<string, object> Properties
        {
            get { return properties; }
        }

        public void Impersonate( User user )
        {
            this.currentUser = user;
        }
    }
}