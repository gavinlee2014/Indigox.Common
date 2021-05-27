using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Indigox.Common.NHibernateFactories.Configuration;
using Indigox.Common.Configuration;


namespace Indigox.Common.NHibernateFactories.Configuration
{
    public class FactoryElement : ConfigElement
    {
        [XmlAttribute("path")]
        public string Path { get; set; }
        [XmlAttribute("assemblyName")]
        public string AssemblyName { get; set; }
        [XmlAttribute("connectionString")]
        public string ConnectionString { get; set; }
        [XmlAttribute("autoBind")]
        public bool AutoBind { get; set; }
      
    }
}
