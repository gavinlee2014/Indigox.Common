using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Indigox.Common.Configuration;

namespace Indigox.Common.StateContainer.Configuration
{
    [XmlRoot( "stateContainer" )]
    public class StateContainerSection : ConfigSection
    {
        private List<ListenerElement> listeners = new List<ListenerElement>();

        [XmlArray( "listeners" )]
        [XmlArrayItem( "listener" )]
        public List<ListenerElement> Listeners
        {
            get { return listeners; }
            set { listeners = value; }
        }
    }
}
