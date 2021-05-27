using System;
using System.Reflection;
using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    internal class ObjectWriter : IObjectWriter
    {
        public ObjectWriter( WriterSettings settings )
        {
            this.settings = settings;
        }

        private WriterSettings settings;

        WriterSettings IObjectWriter.Settings
        {
            get { return this.settings; }
        }

        public void Write( XmlWriter writer, object value )
        {
            if ( value == null )
            {
                return;
            }

            Type type = value.GetType();
            PropertyInfo[] properties = type.GetProperties();

            writer.WriteStartElement( "object" );
            {
                if ( settings.WriteType )
                {
                    writer.WriteAttributeString( "type", this.settings.TypeNameConverter.GetTypeName( type ) );
                }

                foreach ( PropertyInfo property in properties )
                {
                    IPropertyWriter propertyWriter = new ObjectPropertyWriter( this.settings );
                    propertyWriter.Write( writer, value, property.Name );
                }
            }
            writer.WriteEndElement();
        }
    }
}