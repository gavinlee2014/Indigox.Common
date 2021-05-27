using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Indigox.Common.Configuration;

namespace Indigox.Common.EventBus.Configuration
{
    public class EventElement : ConfigElement
    {
        [XmlAttribute("type")]
        public string TypeName { get; set; }

        private List<ListenerElement> listeners;

        [XmlElement("listener", typeof(ListenerElement))]
        public List<ListenerElement> Listeners
        {
            get
            {
                if (listeners == null)
                {
                    listeners = new List<ListenerElement>();
                }
                return listeners;
            }
            set
            {
                this.listeners = value;
            }
        }
    }
}
