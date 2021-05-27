using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Configuration;
using System.Xml.Serialization;

namespace Indigox.Common.DomainModels.Configuration.Generator
{
    public class DBIdGenertatorElement : ConfigElement
    {
        [XmlAttribute("name")]
        public string SerialName { get; set; }

        [XmlAttribute("database")]
        public string DatabaseName { get; set; }

        [XmlAttribute("table")]
        public string TableName { get; set; }
    }
}
