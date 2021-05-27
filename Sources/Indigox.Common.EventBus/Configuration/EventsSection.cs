using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Indigox.Common.Configuration;
using System.Configuration;
using System.IO;

namespace Indigox.Common.EventBus.Configuration
{
    public class EventsSection : ConfigSection
    {

        private List<SourceElement> sources;

        [XmlElement("source", typeof(SourceElement))]
        public List<SourceElement> Sources
        {
            get
            {
                if (sources == null)
                {
                    sources = new List<SourceElement>();
                }
                return sources;
            }
            set
            {
                this.sources = value;
            }
        }


        const string AppConfigPath = "indigo/events";
        const string XmlRootName = "config";

        public static EventsSection LoadFromAppConfig()
        {
            EventsSection section = (EventsSection)ConfigurationManager.GetSection(AppConfigPath);
            return section;
        }

        public static EventsSection LoadFromXmlFile(string filename)
        {
            EventsSection section = null;
            using (StreamReader reader = new StreamReader(filename))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(EventsSection), new XmlRootAttribute(XmlRootName));
                section = (EventsSection)serializer.Deserialize(reader);
            }
            return section;
        }
    }
}
