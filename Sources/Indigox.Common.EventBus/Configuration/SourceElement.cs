 using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Indigox.Common.Configuration;

namespace Indigox.Common.EventBus.Configuration
{
    public class SourceElement : ConfigElement
    {
        [XmlAttribute("type")]
        public string TypeName { get; set; }

        private List<EventElement> events;

        [XmlElement("event", typeof(EventElement))]
        public List<EventElement> Events
        {
            get
            {
                if (events == null)
                {
                    events = new List<EventElement>();
                }
                return events;
            }
            set
            {
                this.events = value;
            }
        }
    }
}
