using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.StateContainer.States
{
    public class ApplicationState : IApplicationState
    {
        public ApplicationState()
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
