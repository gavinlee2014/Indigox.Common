using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Indigox.Common.Logging;

namespace Indigox.Common.Utilities
{
    public static class XmlUtil
    {
        #region serialization

        public static string Serialize( object obj )
        {
            if ( obj == null )
            {
                throw new ArgumentNullException( "obj" );
            }

            Type objectType = obj.GetType();
            XmlSerializer serializer = new XmlSerializer( objectType );
            StringWriter strWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create( strWriter, SerializerWriteSetting );
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add( "", "" );
            serializer.Serialize( xmlWriter, obj, ns );
            string xml = strWriter.ToString();
            return xml;
        }

        public static object Deserializer( Type type, XmlReader reader )
        {
            if ( type == null )
            {
                throw new ArgumentNullException( "type" );
            }
            if ( reader == null )
            {
                throw new ArgumentNullException( "reader" );
            }

            XmlSerializer serializer = new XmlSerializer( type );
            return serializer.Deserialize( reader );
        }

        public static T Deserializer<T>( XmlReader reader )
        {
            if ( reader == null )
            {
                throw new ArgumentNullException( "reader" );
            }

            XmlSerializer serializer = new XmlSerializer( typeof( T ) );
            return (T)serializer.Deserialize( reader );
        }

        public static object DeserializerXml( Type type, string xml )
        {
            StringReader strReader = new StringReader( xml );
            XmlReader xmlReader = XmlReader.Create( strReader );
            return Deserializer( type, xmlReader );
        }

        public static T DeserializerXml<T>( string xml )
        {
            StringReader strReader = new StringReader( xml );
            XmlReader xmlReader = XmlReader.Create( strReader );
            return Deserializer<T>( xmlReader );
        }

        private static XmlWriterSettings serializerWriteSetting;
        private static XmlWriterSettings SerializerWriteSetting
        {
            get
            {
                if ( serializerWriteSetting == null )
                {
                    serializerWriteSetting = new XmlWriterSettings()
                    {
                        OmitXmlDeclaration = true,
                        Indent = true
                    };
                }
                return serializerWriteSetting;
            }
        }

        #endregion serialization

        #region schema validating

        public static void ValidateXmlFile( string xmlPath, Stream xsdReader, string validateRoot )
        {
            XmlSchema schema = XmlSchema.Read( xsdReader, null );
            ValidateXmlFile( xmlPath, schema, validateRoot );
        }

        public static void ValidateXmlFile( string xmlPath, XmlReader xsdReader, string validateRoot )
        {
            XmlSchema schema = XmlSchema.Read( xsdReader, null );
            ValidateXmlFile( xmlPath, schema, validateRoot );
        }

        public static void ValidateXmlFile( string xmlPath, TextReader xsdReader, string validateRoot )
        {
            XmlSchema schema = XmlSchema.Read( xsdReader, null );
            ValidateXmlFile( xmlPath, schema, validateRoot );
        }

        public static void ValidateXmlFile( string xmlPath, string xsdPath, string validateRoot )
        {
            using ( StreamReader xsdReader = new StreamReader( xsdPath ) )
            {
                XmlSchema schema = XmlSchema.Read( xsdReader, null );
                ValidateXmlFile( xmlPath, schema, validateRoot );
            }
        }

        private static void ValidateXmlFile( string xmlPath, XmlSchema schema, string validateRoot )
        {
            XmlReaderSettings settings = GetSchemaValidateSettings( schema );
            using ( XmlReader parentReader = XmlReader.Create( xmlPath ) )
            {
                parentReader.ReadToDescendant( validateRoot );
                XmlReader subReader = parentReader.ReadSubtree();
                using ( XmlReader reader = XmlReader.Create( subReader, settings ) )
                {
                    while ( reader.Read() ) ;
                }
            }
        }

        private static XmlReaderSettings GetSchemaValidateSettings( XmlSchema schema )
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add( schema );
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += OnValidation;
            return settings;
        }

        private static void OnValidation( object sender, ValidationEventArgs args )
        {
            Log.Debug( string.Format( "Line: {1}, {2}. {0}", args.Message, args.Exception.LineNumber, args.Exception.LinePosition ) );
        }

        #endregion schema validating
    }
}