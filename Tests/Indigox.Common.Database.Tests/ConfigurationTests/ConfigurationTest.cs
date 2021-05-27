using Indigox.Common.Data.Configuration;
using NUnit.Framework;

namespace Indigox.Common.Data.Test.Configuration
{
    [TestFixture]
    public class ConfigurationTest
    {
        [Test]
        [Category( "Configuration" )]
        [Description( "" )]
        public void TestConfigurationSection()
        {
            DatabaseSection section = DatabaseSection.Get( "indigo/database" );
            Assert.NotNull( section );
            Assert.NotNull( section.Connections );
            Assert.AreEqual( 1, section.Connections.Count );
        }

        [Test]
        [Category( "Configuration" )]
        [Description( "" )]
        public void TestDefaultConfigurationSection()
        {
            DatabaseSection section = DatabaseSection.Default;
            Assert.NotNull( section );
            Assert.NotNull( section.Connections );
            Assert.AreEqual( 1, section.Connections.Count );
        }
    }
}
