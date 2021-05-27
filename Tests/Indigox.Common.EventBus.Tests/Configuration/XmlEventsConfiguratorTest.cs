using NUnit.Framework;
using Indigox.Common.EventBus.Configuration;

namespace Indigox.Common.EventBus.Test.Configuration
{
    [TestFixture]
    [Category( "Configuration" )]
    public class XmlEventsConfiguratorTest
    {
        //TODO: byYi
        //[Test]
        public void TestConfigureFromAppConfig()
        {
            XmlEventsConfigurator configurator = new XmlEventsConfigurator();
            configurator.Configure();
        }

        [Test]
        public void TestConfigureFromXmlFile()
        {
            XmlEventsConfigurator configurator = new XmlEventsConfigurator( ConfigFile );
            configurator.Configure();
        }


        [Test]
        public void TestLoadConfigSectionFromAppConfig()
        {
            EventsSection section = EventsSection.LoadFromAppConfig();
            //System.Diagnostics.Debug.WriteLine( section.ToXml() );
        }

        [Test]
        public void TestLoadConfigSectionFromXmlFile()
        {
            EventsSection section = EventsSection.LoadFromXmlFile( ConfigFile );
            Assert.NotNull(section);
            //System.Diagnostics.Debug.WriteLine( section.ToXml() );
        }

        private static readonly string ConfigFile = "config\\events.xml";
    }
}
