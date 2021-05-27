using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Session.Common
{
    internal class ThreadStaticSessionBinder : ICurrentSessionBinder
    {
        [ThreadStatic]
        private ISession session;

        public ISession Session
        {
            get
            {
                return session;
            }
            set
            {
                session = value;
            }
        }
    }
}