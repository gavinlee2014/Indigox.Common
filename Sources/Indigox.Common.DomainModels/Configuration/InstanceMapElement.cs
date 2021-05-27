using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Configuration;
using System.Xml.Serialization;

namespace Indigox.Common.DomainModels.Configuration
{
    public class InstanceMapElement : ConfigElement
    {
        [XmlAttribute("interface")]
        public string Interface { get; set; }

        [XmlAttribute("instance")]
        public string Instance { get; set; }
    }
}
