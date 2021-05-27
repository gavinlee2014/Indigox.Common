using System;
using System.Xml;
using System.Xml.Serialization;

namespace Indigox.Common.NHibernate.Extension.Serialization.XmlSerialization
{
    internal class Base64XmlSerializer : IXmlSerializer
    {
        private static Base64XmlSerializer instance = new Base64XmlSerializer();

        private Base64XmlSerializer()
        {
        }

        public static Base64XmlSerializer Instance
        {
            get { return instance; }
        }

        public object Deserialize( Type type, XmlReader reader )
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( typeof( byte[] ) );
            object bytesObj = serializer.Deserialize( reader );

            byte[] bytes = (byte[])bytesObj;
            object obj = BinarySerializer.Deserialize( bytes );
            return obj;
        }

        public void Serialize( object value, XmlWriter writer )
        {
            byte[] bytes = BinarySerializer.Serialize( value );

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( typeof( byte[] ) );
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces( new XmlQualifiedName[] { new XmlQualifiedName( "", "" ) } );
            serializer.Serialize( writer, value, ns );
        }
    }
}