using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    internal class ObjectPropertyWriter : IPropertyWriter
    {
        public ObjectPropertyWriter( WriterSettings settings )
        {
            this.settings = settings;
        }

        private WriterSettings settings;

        WriterSettings IPropertyWriter.Settings
        {
            get { return this.settings; }
        }

        public void Write( XmlWriter writer, object value, string propertyName )
        {
            writer.WriteStartElement( "property" );
            {
                writer.WriteAttributeString( "name", propertyName );

                WritePropertyValue( writer, value, propertyName );
            }
            writer.WriteEndElement();
        }

        private void WritePropertyValue( XmlWriter writer, object value, string propertyName )
        {
            object propertyValue = value.GetType().GetProperty( propertyName ).GetValue( value, null );

            if ( propertyValue != null )
            {
                if ( this.settings.WriteType )
                {
                    writer.WriteAttributeString( "type", this.settings.TypeNameConverter.GetTypeName( propertyValue.GetType() ) );
                }

                IObjectWriter propertyWriter = this.settings.GetWriter( propertyValue.GetType() );
                propertyWriter.Write( writer, propertyValue );
            }
        }
    }
}