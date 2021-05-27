using System;
using Indigox.Common.Membership;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.StateContainer.CurrentUserProviders
{
    /// <summary>
    /// usefull for test program
    /// </summary>
    public class MutableCurrentUserProvider : ICurrentUserProvider
    {
        private static IOrganizationalPerson currentUser;

        public static void SetCurrentUser( string username )
        {
            currentUser = OrganizationalPerson.GetOrganizationalPersonByAccount( username );
        }

        public string GetCurrentUser()
        {
            if ( currentUser == null )
            {
                return null;
            }
            else
            {
                return currentUser.AccountName;
            }
        }
    }
}