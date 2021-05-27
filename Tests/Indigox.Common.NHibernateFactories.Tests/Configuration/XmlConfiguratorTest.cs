using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Indigox.Common.NHibernateFactories.Configuration;

namespace Indigox.Common.NHibernateFactories.Test.Configuration
{
    [TestFixture]
    public class XmlConfiguratorTest
    {
        //TODO: byYi
        //[Test]
        public void TestConfigureFromAppConfig()
        {
            XmlConfigurator configurator = new XmlConfigurator();
            configurator.Configure();
        }

        [Test]
        public void TestConfigureFromXmlFile()
        {
            XmlConfigurator configurator = new XmlConfigurator(ConfigFile);
            configurator.Configure();
        }
        [Test]
        public void TestLoadConfigSectionFromAppConfig()
        {
            FactoriesSection section = FactoriesSection.LoadFromAppConfig();
            //System.Diagnostics.Debug.WriteLine( section.ToXml() );
        }

        [Test]
        public void TestLoadConfigSectionFromXmlFile()
        {
            FactoriesSection section = FactoriesSection.LoadFromXmlFile(ConfigFile);
            Assert.NotNull(section);
            //System.Diagnostics.Debug.WriteLine( section.ToXml() );
        }

        private static readonly string ConfigFile = "config\\factories.xml";
    }
}


