using System;
using System.Collections;
using NHibernate.Context;
using NHibernate.Engine;

namespace Indigox.Common.NHibernate.Extension.Context
{
    [Serializable]
    public class StaticSessionContext : MapBasedSessionContext
    {
        private static IDictionary map;

        public StaticSessionContext( ISessionFactoryImplementor factory )
            : base( factory )
        {
        }

        protected override IDictionary GetMap()
        {
            return map;
        }

        protected override void SetMap( IDictionary value )
        {
            map = value;
        }
    }
}