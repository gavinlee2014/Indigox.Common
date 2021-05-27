using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Indigox.Common.Serialization.Test
{
    [TestFixture]
    public class XmlSerializerTest
    {
        [Test]
        [TestCaseSource( "SerializeObjects" )]
        public void TestSerialize( object value )
        {
            SerializerSettings settings = new SerializerSettings();
            settings.Indent = true;
            XmlSerializer serializer = new XmlSerializer( settings );
            string xml = serializer.Serialize( value );
            Console.WriteLine( xml );
        }

        static XmlSerializerTest()
        {
            IList<Hashtable> listHashtable = new List<Hashtable>();
            for ( int i = 0 ; i < 3 ; i++ )
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add( "sharp", new Sharp( "squart" ) );
                hashtable.Add( "id", "item_" + i );
                hashtable.Add( "guid", Guid.NewGuid() );
                listHashtable.Add( hashtable );
            }

            IList<IDictionary<string, object>> listDictionary = new List<IDictionary<string, object>>();
            for ( int i = 0 ; i < 3 ; i++ )
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add( "sharp", new Sharp( "squart" ) );
                dictionary.Add( "id", "item_" + i );
                dictionary.Add( "guid", Guid.NewGuid() );
                listDictionary.Add( dictionary );
            }

            SerializeObjects = new object[] { listHashtable, listDictionary };
        }

        static object[] SerializeObjects;

        class Sharp
        {
            public Sharp( string name )
            {
                this.Name = name;
            }
            public string Name { get; set; }
        }
    }
}
