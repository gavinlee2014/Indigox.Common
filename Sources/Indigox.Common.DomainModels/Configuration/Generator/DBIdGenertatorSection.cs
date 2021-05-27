using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Configuration;
using System.Xml.Serialization;

namespace Indigox.Common.DomainModels.Configuration.Generator
{
    [XmlRoot("dbIdGenertators")]
    public class DBIdGenertatorSection : ConfigSection
    {
        [XmlElement("dbIdGenertator")]
        public List<DBIdGenertatorElement> DBIdGenertators { get; set; }
    }
}
