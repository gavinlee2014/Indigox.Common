using System.Collections.Generic;
using Indigox.Common.Membership;

namespace Indigox.Common.Session.Common
{
    /// <summary>
    /// usefull for test program
    /// </summary>
    public class MutableSession : ISession
    {
        User currentUser;
        Dictionary<string, object> properties = new Dictionary<string, object>();

        public User CurrentUser
        {
            get { return this.currentUser; }
            set { this.currentUser = value; }
        }

        public Dictionary<string, object> Properties
        {
            get { return properties; }
        }
    }
}