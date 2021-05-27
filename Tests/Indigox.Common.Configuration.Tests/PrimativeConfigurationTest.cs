using System;
using Indigox.Common.Configuration.Test.Configs;
using NUnit.Framework;

namespace Indigox.Common.Configuration.Test
{
    [TestFixture]
    public class PrimativeConfigurationTest
    {
        #region SetUp and TearDown methods

        /// <summary>
        /// run before each test method
        /// </summary>
        [SetUp]
        public void SetUp()
        {
        }

        /// <summary>
        /// run after each test method
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            OrganizationConfigurations.Clear();
        }

        /// <summary>
        /// run before test fixture start
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        /// <summary>
        /// run after test fixture quit
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        #endregion SetUp and TearDown methods

        [Test]
        public void TestBasicConfig()
        {
            // Object and Collection Initialization
            // This code style require complied by VS 2008 or higher and .net framework 3.0 or higher
            OrganizationConfigSection config = new OrganizationConfigSection()
            {
                Extends = new ExtendPropertiesElement()
                {
                    IntVal = int.MaxValue,
                    ShortVal = short.MaxValue,
                    LongVal = long.MaxValue,
                    BoolVal = true,
                    DoubleVal = double.MaxValue,
                    FloatVal = float.MaxValue,
                    ByteVal = 0xFF,
                    BytesVal = new byte[] { 0x11, 0x22, 0x33 },
                    DateTimeVal = DateTime.Now,
                    GuidVal = Guid.NewGuid(),
                    StringVal = "hello,world!",
                    CharVal = 'c',
                    EnumStateVal = State.Middle
                }
            };

            Console.WriteLine( config.ToXml() );
        }

        //TODO: byYi
        //[Test]
        public void TestLoadConfigFromConfigFile()
        {
            OrganizationConfigurator configurator = new OrganizationConfigurator();
            configurator.LoadFromConfig( "contoso/extention" );
            configurator.Configure();

            OrganizationConfigSection config = OrganizationConfigurations.Instance.GetConfiguration();
            Console.WriteLine( config.ToXml() );
        }

        [Test]
        public void TestLoadConfigFromXmlFile()
        {
            OrganizationConfigurator configurator = new OrganizationConfigurator();
            configurator.LoadFromXmlFile( "PrimativeConfig.xml" );
            configurator.Configure();

            OrganizationConfigSection config = OrganizationConfigurations.Instance.GetConfiguration();
            Console.WriteLine( config.ToXml() );
        }
    }
}
