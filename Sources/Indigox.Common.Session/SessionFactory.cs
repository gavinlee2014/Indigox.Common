using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;
using Indigox.Common.Logging;
using Indigox.Common.Session.App;
using Indigox.Common.Session.Web;
using Indigox.Common.Utilities;

namespace Indigox.Common.Session
{
    internal class SessionFactory
    {
        public static ISession CreateSession()
        {
            string sessionType = GetSessionTypeNameFromConfiguration();
            if ( sessionType == null )
            {
                return CreateDefaultSession();
            }
            else
            {
                Log.Debug( string.Format( "[{0}] Create specified session: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    sessionType ) );
                return (ISession)ReflectUtil.CreateInstance( sessionType );
            }
        }

        private static ISession CreateDefaultSession()
        {
            if ( ApplicationInfo.IsWebApplication )
            {
                Log.Debug( string.Format( "[{0}] Create default session: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    typeof( HttpContextSession ).FullName) );
                return new HttpContextSession();
            }
            else
            {
                Log.Debug( string.Format( "[{0}] Create default session: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    typeof( AppSession ).FullName ) );
                return new AppSession();
            }
        }

        private static string GetSessionTypeNameFromConfiguration()
        {
            return ConfigurationManager.AppSettings[ "SessionType" ];
        }
    }
}