using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Indigox.Common.Configuration;

namespace Indigox.Common.EventBus.Configuration
{
    public class ListenerElement : ConfigElement
    {

        [XmlAttribute("type")]
        public string TypeName { get; set; }

        [XmlAttribute("method")]
        public string MethodName { get; set; }
    }
}
