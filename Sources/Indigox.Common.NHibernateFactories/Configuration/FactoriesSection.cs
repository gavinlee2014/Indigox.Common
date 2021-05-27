using System;
using System.Text;
using System.Xml.Serialization;
using Indigox.Common.NHibernateFactories.Configuration;
using System.Configuration;
using System.IO;
using Indigox.Common.Configuration;
using System.Collections.Generic;

namespace Indigox.Common.NHibernateFactories.Configuration
{
    public class FactoriesSection : ConfigSection
    {

        private List<FactoryElement> factories;

        [XmlElement("factory", typeof(FactoryElement))]
        public List<FactoryElement> Factories
        {
            get
            {
                if (factories == null)
                {
                    factories = new List<FactoryElement>();
                }
                return factories;
            }
            set
            {
                this.factories = value;
            }
        }


        const string AppConfigPath = "indigo/factories";
        const string XmlRootName = "config";

        public static FactoriesSection LoadFromAppConfig()
        {
            FactoriesSection section = (FactoriesSection)ConfigurationManager.GetSection(AppConfigPath);
            return section;
        }

        public static FactoriesSection LoadFromXmlFile(string filename)
        {
            FactoriesSection section = null;
            using (StreamReader reader = new StreamReader(filename))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(FactoriesSection), new XmlRootAttribute(XmlRootName));
                section = (FactoriesSection)serializer.Deserialize(reader);
            }
            return section;
        }
    }
}
