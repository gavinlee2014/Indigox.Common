using System;
using System.Xml;
using Indigox.Common.NHibernate.Extension.Reflection;
using Indigox.Common.NHibernate.Extension.Serialization.XmlSerialization;
using Indigox.Common.NHibernate.Extension.Utils;

namespace Indigox.Common.NHibernate.Extension.Serialization
{
    internal class XmlSerializer
    {
        public static void Serialize( object value, XmlWriter writer )
        {
            if ( value == null )
            {
                return;
            }

            Type type = value.GetType();

            writer.WriteStartElement( "object" );
            writer.WriteAttributeString( "class", TypeName.Instance.GetTypeName( type ) );

            if ( TypeInfo.IsDictionary( type ) || TypeInfo.IsGenericDictionary( type ) )
            {
                writer.WriteAttributeString( "type", "dictionary" );
                DictionaryXmlSerializer.Instance.Serialize( value, writer );
            }
            else if ( TypeInfo.IsCollection( type ) || TypeInfo.IsGenericCollection( type ) )
            {
                writer.WriteAttributeString( "type", "collection" );
                CollectionXmlSerializer.Instance.Serialize( value, writer );
            }
            else if ( CanXmlSerialize( value ) )
            {
                writer.WriteAttributeString( "type", "object" );
                ObjectXmlSerializer.Instance.Serialize( value, writer );
            }
            else
            {
                writer.WriteAttributeString( "type", "base64" );
                Base64XmlSerializer.Instance.Serialize( value, writer );
            }

            writer.WriteEndElement(); // </object>
        }

        private static bool CanXmlSerialize( object value )
        {
            Type type = value.GetType();
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( type );
                return true;
            }
            catch ( Exception )
            {
                return false;
            }
        }

        public static object Deserialize( XmlReader xmlreader )
        {
            object obj = null;
            xmlreader.ReadToDescendant( "object" );
            string type = xmlreader.GetAttribute( "type" );
            string clazz = xmlreader.GetAttribute( "class" );
            Type objectType = TypeName.Instance.GetType( clazz );

            xmlreader.Read();
            using ( XmlReader subxmlreader = xmlreader.ReadSubtree() )
            {
                if ( type == "dictionary" )
                {
                    obj = DictionaryXmlSerializer.Instance.Deserialize( objectType, subxmlreader );
                }
                else if ( type == "collection" )
                {
                    obj = CollectionXmlSerializer.Instance.Deserialize( objectType, subxmlreader );
                }
                else if ( type == "base64" )
                {
                    obj = Base64XmlSerializer.Instance.Deserialize( objectType, subxmlreader );
                }
                else
                {
                    obj = ObjectXmlSerializer.Instance.Deserialize( objectType, subxmlreader );
                }
            }
            return obj;
        }
    }
}