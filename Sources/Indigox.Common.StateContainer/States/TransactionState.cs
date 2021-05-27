using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;

namespace Indigox.Common.StateContainer.States
{
    public class TransactionState : ITransactionState
    {
        public TransactionState()
        {
            this.properties = new Dictionary<string, object>();
        }

        private Dictionary<string, object> properties;

        public object this[ string key ]
        {
            get
            {
                if ( !properties.ContainsKey( key ) )
                    return null;
                return properties[ key ];
            }
            set
            {
                properties[ key ] = value;
            }
        }
    }
}
