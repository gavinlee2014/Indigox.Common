using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using Indigox.Common.Utilities;

namespace Indigox.Common.Session
{
    public class SessionManager
    {
        private static ICurrentSessionBinder SessionBinder = CurrentSessionBinderFactory.Create();

        /// <summary>
        /// 获取当前 Session
        /// </summary>
        /// <returns></returns>
        public static ISession GetCurrentSession()
        {
            if ( SessionBinder.Session == null )
            {
                SessionBinder.Session = SessionFactory.CreateSession();
            }
            return SessionBinder.Session;
        }

        public static ISession CurrentSession
        {
            get
            {
                return GetCurrentSession();
            }
        }

        /// <summary>
        /// 设置当前 Session
        /// </summary>
        /// <param name="session"></param>
        public static void SetCurrentSession( ISession session )
        {
            SessionBinder.Session = session;
        }

        /// <summary>
        /// 抛弃当前 Session
        /// </summary>
        public static void AbandonCurrentSession()
        {
            SessionBinder.Session = null;
        }
    }
}