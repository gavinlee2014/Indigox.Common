using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Indigox.Common.Configuration.Test.Configs;
using NUnit.Framework;

namespace Indigox.Common.Configuration.Test
{
    [TestFixture]
    internal class ConfigDictionaryElementTest
    {
        [SetUp]
        public void OnSetUp()
        {
            datas = new Dictionary<string, object>();
            datas.Add( "string", "this is an test string." );
            datas.Add( "nullString", null );
            datas.Add( "int", 10 );
            datas.Add( "float", 10.0F );
            datas.Add( "complex", new UserElement() { Name = "user" } );
            datas.Add( "nullComplex", null );
        }

        private Dictionary<string, object> datas;

        [Test]
        public void TestSerialize()
        {
            ConfigDictionaryElement<object> element = new ConfigDictionaryElement<object>( datas );
            XmlSerializer serizlizer = new XmlSerializer( typeof( ConfigDictionaryElement<object> ) );
            serizlizer.Serialize( Console.Out, element );
            Console.WriteLine();
        }

        [Test]
        public void TestDeserialize()
        {
            string xml = @"
<ConfigDictionaryElementOfObject>
  <string type=""string"">this is an test string.</string>
  <nullString isnull=""true"" />
  <int type=""int"">10</int>
  <float type=""float"">10</float>
  <complex type=""Indigox.Common.Configuration.Test.Configs.UserElement, Indigox.Common.Configuration.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"">
    <UserElement xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" name=""user"" />
  </complex>
  <nullComplex isnull=""true"" />
</ConfigDictionaryElementOfObject>
";

            ConfigDictionaryElement<object> element = null;
            using ( StringReader strReader = new StringReader( xml ) )
            using ( XmlReader xmlReader = XmlReader.Create( strReader ) )
            {
                XmlSerializer serizlizer = new XmlSerializer( typeof( ConfigDictionaryElement<object> ) );
                element = (ConfigDictionaryElement<object>)serizlizer.Deserialize( xmlReader );
            }
            foreach ( string key in datas.Keys )
            {
                if ( key == "complex" )
                    Assert.AreEqual( ( (UserElement)datas[ key ] ).Name, ( (UserElement)element[ key ] ).Name );
                else
                    Assert.AreEqual( datas[ key ], element[ key ] );
            }
        }
    }
}
