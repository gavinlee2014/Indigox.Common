using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Indigox.Common.Serialization.Xml;

namespace Indigox.Common.Serialization.Test
{
    [TestFixture]
    public class TypeNameConverterTest
    {
        [Test]
        [TestCase( typeof( int ) )]
        [TestCase( typeof( List<object> ) )]
        [TestCase( typeof( Dictionary<string, object> ) )]
        [TestCase( typeof( List<Dictionary<string, object>> ) )]
        public void TestGetTypeName( Type type )
        {
            TypeNameConverter converter = new TypeNameConverter();

            Console.WriteLine( "---------" );
            Console.WriteLine( converter.GetTypeName( type ) );

            Console.WriteLine( "---------" );
            Console.WriteLine( type.AssemblyQualifiedName );

            Assert.AreEqual( type, Type.GetType( converter.GetTypeName( type ) ) );
        }
    }
}
