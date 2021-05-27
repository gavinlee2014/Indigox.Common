using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Indigox.Common.Session.Web
{
    internal class HttpContextCurrentSessionBinder : ICurrentSessionBinder
    {
        private const string HTTPCONTEXT_SESSION_KEY = "__Indigox_Common_Session__";
        private const string HASHTABLE_SESSION_KEY = "Session";

        public ISession Session
        {
            get
            {
                Hashtable ht = GetCachedItems();
                return (ISession)ht[ HASHTABLE_SESSION_KEY ];
            }
            set
            {
                Hashtable ht = GetCachedItems();
                ht[ HASHTABLE_SESSION_KEY ] = value;
            }
        }

        private static Hashtable GetCachedItems()
        {
            if ( HttpContext.Current.Session[ HTTPCONTEXT_SESSION_KEY ] == null )
            {
                HttpContext.Current.Session[ HTTPCONTEXT_SESSION_KEY ] = new Hashtable();
            }
            Hashtable ht = (Hashtable)HttpContext.Current.Session[ HTTPCONTEXT_SESSION_KEY ];
            return ht;
        }
    }
}