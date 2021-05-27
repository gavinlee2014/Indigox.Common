using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Indigox.Common.Configuration;

namespace Indigox.Common.DomainModels.NHibernateImpl
{
    public class FactoryElement : ConfigElement
    {
        [XmlAttribute("path")]
        public string Path { get; set; }
        [XmlAttribute("assemblyName")]
        public string AssemblyName { get; set; }

      
    }
}
