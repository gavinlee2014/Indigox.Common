using System;
using System.IO;
using System.Text;
using System.Xml;
using Indigox.Common.Serialization.Xml;

namespace Indigox.Common.Serialization
{
    public class XmlSerializer : ISerializer
    {

        private SerializerSettings settings;

        public XmlSerializer()
            : this( new SerializerSettings() )
        {

        }

        public XmlSerializer( SerializerSettings settings )
        {
            this.settings = settings;
        }

        public string Serialize( object value )
        {
            if ( value == null )
            {
                return null;
            }

            WriterSettings serializerSettings = new WriterSettings();
            serializerSettings.WriteType = this.settings.WriteType;
            if ( !this.settings.WriteTypeAlias )
                serializerSettings.TypeNameConverter = new TypeNameConverter();

            IObjectWriter writer = serializerSettings.GetWriter( value.GetType() );

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = true;
            xmlWriterSettings.Indent = this.settings.Indent;

            StringBuilder temp = new StringBuilder();
            using ( StringWriter strWriter = new StringWriter( temp ) )
            using ( XmlWriter xmlWriter = XmlWriter.Create( strWriter, xmlWriterSettings ) )
            {
                writer.Write( xmlWriter, value );
            }
            return temp.ToString();
        }

        public object Deserialize( string xml )
        {
            throw new NotImplementedException();
        }

        public object Deserialize( Type type, string xml )
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>( string xml )
        {
            return (T)Deserialize( typeof( T ), xml );
        }
    }
}