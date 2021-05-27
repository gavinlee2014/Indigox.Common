using System;
using System.Xml;
using System.Xml.Serialization;

namespace Indigox.Common.NHibernate.Extension.Serialization.XmlSerialization
{
    internal class ObjectXmlSerializer : IXmlSerializer
    {
        private static ObjectXmlSerializer instance = new ObjectXmlSerializer();

        private ObjectXmlSerializer()
        {
        }

        public static ObjectXmlSerializer Instance
        {
            get { return instance; }
        }

        public object Deserialize( Type type, XmlReader reader )
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( type );
            object obj = serializer.Deserialize( reader );
            return obj;
        }

        public void Serialize( object value, XmlWriter writer )
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( value.GetType() );
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces( new XmlQualifiedName[] { new XmlQualifiedName( "", "" ) } );
            serializer.Serialize( writer, value, ns );
        }
    }
}