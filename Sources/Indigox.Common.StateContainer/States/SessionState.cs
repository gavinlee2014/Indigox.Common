using System;
using System.Collections.Generic;
using Indigox.Common.Membership;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.StateContainer.States
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
        private IOrganizationalPerson currentUser;
        private ICurrentUserProvider currentUserProvider;

        public virtual IOrganizationalPerson User
        {
            get
            {
                string username = currentUserProvider.GetCurrentUser();
                if ( string.IsNullOrEmpty( username ) )
                {
                    return null;
                }
                if ( this.currentUser == null || !StringComparer.CurrentCultureIgnoreCase.Equals( this.currentUser.AccountName, username ) )
                {
                    IOrganizationalPerson user = OrganizationalPerson.GetOrganizationalPersonByAccount( username );
                    this.currentUser = user;
                }
                return this.currentUser;
            }
        }

        public object this[ string key ]
        {
            get
            {
                if ( !properties.ContainsKey( key ) )
                    return null;
                return properties[ key ];
            }
            set
            {
                properties[ key ] = value;
            }
        }
    }
}