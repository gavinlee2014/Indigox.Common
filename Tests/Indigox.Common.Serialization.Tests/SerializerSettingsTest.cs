using System;
using System.Collections;
using System.Collections.Generic;
using Indigox.Common.Serialization.Xml;
using NUnit.Framework;

namespace Indigox.Common.Serialization.Test
{
    [TestFixture]
    public class SerializerSettingsTest
    {
        [Test]
        [TestCase( typeof( int ), typeof( SimpleValueWriter ) )]
        [TestCase( typeof( bool ), typeof( SimpleValueWriter ) )]
        [TestCase( typeof( float ), typeof( SimpleValueWriter ) )]
        [TestCase( typeof( double ), typeof( SimpleValueWriter ) )]
        [TestCase( typeof( short ), typeof( SimpleValueWriter ) )]
        [TestCase( typeof( string ), typeof( SimpleValueWriter ) )]
        [TestCase( typeof( DateTime ), typeof( SimpleValueWriter ) )]
        [TestCase( typeof( Guid ), typeof( SimpleValueWriter ) )]
        [TestCase( typeof( Object ), typeof( ObjectWriter ) )]
        [TestCase( typeof( Sharp ), typeof( ObjectWriter ) )]
        [TestCase( typeof( ArrayList ), typeof( CollectionWriter ) )]
        [TestCase( typeof( List<object> ), typeof( CollectionWriter ) )]
        [TestCase( typeof( Hashtable ), typeof( DictionaryWriter ) )]
        [TestCase( typeof( Dictionary<string, object> ), typeof( GenericDictionaryWriter ) )]
        public void TestGetWriter( Type type, Type expectedWriterType )
        {
            WriterSettings settings = new WriterSettings();
            Assert.IsInstanceOf( expectedWriterType, settings.GetWriter( type ) );
        }

        class Sharp
        {

        }
    }
}
