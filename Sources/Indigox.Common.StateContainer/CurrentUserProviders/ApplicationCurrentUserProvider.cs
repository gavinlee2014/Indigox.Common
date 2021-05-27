using System;

namespace Indigox.Common.StateContainer.CurrentUserProviders
{
    public class ApplicationCurrentUserProvider : ICurrentUserProvider
    {
        public string GetCurrentUser()
        {
            try
            {
                return Environment.UserName;
            }
            catch ( Exception )
            {
                return null;
            }
        }
    }
}