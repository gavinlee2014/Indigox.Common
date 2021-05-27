using System.Diagnostics;
using Indigox.Common.Configuration.Test.Configs;
using NUnit.Framework;

namespace Indigox.Common.Configuration.Test
{
    [TestFixture]
    public class OrganizationConfiguratorTest
    {
        [TearDown]
        public void OnTearDown()
        {
            OrganizationConfigurations.Clear();
        }

        [Test]
        public void TestBasicConfig()
        {
            OrganizationConfigurator configurator = new OrganizationConfigurator();
            configurator.Organization()
                .SetBoss( "john", "boss" )
                .SetDepartBoss( "manager" )
                .AddUser( "el1" )
                .AddUser( "el2" )
                .AddUser( "el3" );
            configurator.Configure();

            Debug.WriteLine( OrganizationConfigurations.Instance.GetConfiguration().ToXml() );

            OrganizationConfigSection root = OrganizationConfigurations.Instance.GetConfiguration();

            Assert.IsNotNull( root.Boss );
            Assert.AreEqual( "john", root.Boss.Name );
            Assert.IsNotNull( root.DepartBoss );
            Assert.AreEqual( "manager", root.DepartBoss.Name );
            Assert.IsNull( root.BigBoss );
            Assert.AreEqual( 3, root.Users.Count );
            Assert.AreEqual( "el1", root.Users[ 0 ].Name );
            Assert.AreEqual( "el2", root.Users[ 1 ].Name );
            Assert.AreEqual( "el3", root.Users[ 2 ].Name );
        }

        //TODO: byYi
        //[Test]
        public void TestLoadFromConfig ()
        {
            OrganizationConfigurator configurator = new OrganizationConfigurator();
            configurator.LoadFromConfig( "contoso/extention" );
            configurator.Configure();

            OrganizationConfigSection root = OrganizationConfigurations.Instance.GetConfiguration();

            Assert.IsNotNull( root.Boss );
            Assert.AreEqual( "john", root.Boss.Name );
            Assert.IsNotNull( root.DepartBoss );
            Assert.AreEqual( "manager", root.DepartBoss.Name );
            Assert.IsNull( root.BigBoss );
            Assert.AreEqual( 4, root.Users.Count );
            Assert.AreEqual( "el1", root.Users[ 0 ].Name );
            Assert.AreEqual( "el2", root.Users[ 1 ].Name );
            Assert.AreEqual( "el3", root.Users[ 2 ].Name );
        }

        [Test]
        public void TestLoadFromXML()
        {
            OrganizationConfigurator configurator = new OrganizationConfigurator();
            configurator.LoadFromXmlFile( "ExtentionElementConfig.xml" );
            configurator.Configure();

            OrganizationConfigSection root = OrganizationConfigurations.Instance.GetConfiguration();

            Assert.IsNotNull( root.Boss );
            Assert.AreEqual( "john", root.Boss.Name );
            Assert.IsNotNull( root.DepartBoss );
            Assert.AreEqual( "manager", root.DepartBoss.Name );
            Assert.IsNull( root.BigBoss );
            Assert.AreEqual( 4, root.Users.Count );
            Assert.AreEqual( "el1", root.Users[ 0 ].Name );
            Assert.AreEqual( "el2", root.Users[ 1 ].Name );
            Assert.AreEqual( "el3", root.Users[ 2 ].Name );
        }
    }
}
