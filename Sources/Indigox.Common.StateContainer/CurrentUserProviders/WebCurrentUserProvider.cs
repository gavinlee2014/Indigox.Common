using System.Web;
using Indigox.Common.Logging;

namespace Indigox.Common.StateContainer.CurrentUserProviders
{
    public class WebCurrentUserProvider : ICurrentUserProvider
    {
        public string GetCurrentUser()
        {
            Log.Debug( string.Format( "[CurrentUserInfo] CurrentUser: {1} - ({0})",
                ( HttpContext.Current.User.Identity.IsAuthenticated ? "Authenticated" : "UnAuthenticated" ),
                HttpContext.Current.User.Identity.Name ) );

            if ( !HttpContext.Current.User.Identity.IsAuthenticated )
            {
                return null;
            }

            string username = HttpContext.Current.User.Identity.Name;
            string account = username;

            if ( username.Contains( "\\" ) )
            {
                account = username.Substring( username.IndexOf( "\\" ) + 1 );
            }

            Log.Debug( string.Format( "[WebCurrentUserProvider] current user account: {0}", account ) );

            return account;
        }
    }
}