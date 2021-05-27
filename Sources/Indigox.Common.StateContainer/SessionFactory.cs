using System.Configuration;
using System.Threading;
using Indigox.Common.Logging;
using Indigox.Common.StateContainer.CurrentUserProviders;
using Indigox.Common.StateContainer.States;
using Indigox.Common.Utilities;

namespace Indigox.Common.StateContainer
{
    public class SessionFactory
    {
        public static ICurrentUserProvider CreateCurrentUserProvider()
        {
            string currentUserProviderType = GetCurrentUserProviderTypeFromAppConfig();
            if ( currentUserProviderType == null )
            {
                return CreateDefaultCurrentUserProvider();
            }
            else
            {
                Log.Debug( string.Format( "[{0}] CurrentUserProvider: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    currentUserProviderType ) );
                return (ICurrentUserProvider)ReflectUtil.CreateInstance( currentUserProviderType );
            }
        }

        private static ICurrentUserProvider CreateDefaultCurrentUserProvider()
        {
            if ( ApplicationInfo.IsWebApplication )
            {
                Log.Debug( string.Format( "[{0}] CurrentUserProvider: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    typeof( WebCurrentUserProvider ).FullName ) );
                return new WebCurrentUserProvider();
            }
            else
            {
                Log.Debug( string.Format( "[{0}] CurrentUserProvider: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    typeof( ApplicationCurrentUserProvider ).FullName ) );
                return new ApplicationCurrentUserProvider();
            }
        }

        private static string GetCurrentUserProviderTypeFromAppConfig()
        {
            return ConfigurationManager.AppSettings[ "CurrentUserProviderType" ];
        }
    }
}