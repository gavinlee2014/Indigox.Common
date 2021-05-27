using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Indigox.Common.StateContainer.StateContexts
{
    /// <summary>
    /// Bind session context to HttpContext.Current.Session
    /// </summary>
    internal class WebStateContext : StateContext
    {
        private const string HttpContextSessionKey = "__Indigox_Common_Session__";
        private const string HashtableApplicationKey = "Application";
        private const string HashtableSessionKey = "Session";
        private const string HashtableTransactionKey = "Transaction";

        public override IApplicationState Application
        {
            get
            {
                Hashtable ht = GetHttpApplicationItems();
                return (IApplicationState)ht[ HashtableApplicationKey ];
            }
            protected set
            {
                Hashtable ht = GetHttpApplicationItems();
                ht[ HashtableApplicationKey ] = value;
            }
        }

        public override ISessionState Session
        {
            get
            {
                Hashtable ht = GetHttpSessionItems();
                return (ISessionState)ht[ HashtableSessionKey ];
            }
            protected set
            {
                Hashtable ht = GetHttpSessionItems();
                ht[ HashtableSessionKey ] = value;
            }
        }

        public override ITransactionState Transaction
        {
            get
            {
                Hashtable ht = GetHttpContextItems();
                return (ITransactionState)ht[ HashtableTransactionKey ];
            }
            protected set
            {
                Hashtable ht = GetHttpContextItems();
                ht[ HashtableTransactionKey ] = value;
            }
        }

        private static Hashtable GetHttpApplicationItems()
        {
            if ( HttpContext.Current.Application[ HttpContextSessionKey ] == null )
            {
                HttpContext.Current.Application[ HttpContextSessionKey ] = new Hashtable();
            }
            Hashtable ht = (Hashtable)HttpContext.Current.Application[ HttpContextSessionKey ];
            return ht;
        }

        private static Hashtable GetHttpSessionItems()
        {
            if ( HttpContext.Current.Session[ HttpContextSessionKey ] == null )
            {
                HttpContext.Current.Session[ HttpContextSessionKey ] = new Hashtable();
            }
            Hashtable ht = (Hashtable)HttpContext.Current.Session[ HttpContextSessionKey ];
            return ht;
        }

        private static Hashtable GetHttpContextItems()
        {
            if ( HttpContext.Current.Items[ HttpContextSessionKey ] == null )
            {
                HttpContext.Current.Items[ HttpContextSessionKey ] = new Hashtable();
            }
            Hashtable ht = (Hashtable)HttpContext.Current.Items[ HttpContextSessionKey ];
            return ht;
        }
    }
}