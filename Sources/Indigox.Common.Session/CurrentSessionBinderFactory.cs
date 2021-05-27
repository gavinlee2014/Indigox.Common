using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Indigox.Common.Logging;
using Indigox.Common.Session.Common;
using Indigox.Common.Session.Web;

namespace Indigox.Common.Session
{
    internal class CurrentSessionBinderFactory
    {
        public static ICurrentSessionBinder Create()
        {
            if ( ApplicationInfo.IsWebApplication )
            {
                Log.Debug( string.Format( "[{0}] Create CurrentSessionBinder: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    typeof( HttpContextCurrentSessionBinder ).FullName ) );
                return new HttpContextCurrentSessionBinder();
            }
            else
            {
                Log.Debug( string.Format( "[{0}] Create CurrentSessionBinder: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    typeof( ThreadStaticSessionBinder ).FullName ) );
                return new ThreadStaticSessionBinder();
            }
        }
    }
}