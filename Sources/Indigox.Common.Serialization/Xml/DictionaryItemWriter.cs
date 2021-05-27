using System.Collections;
using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    internal class DictionaryItemWriter : IPropertyWriter
    {
        public DictionaryItemWriter( WriterSettings settings )
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
            writer.WriteStartElement( "item" );
            {
                writer.WriteAttributeString( "name", propertyName );

                WritePropertyValue( writer, value, propertyName );
            }
            writer.WriteEndElement();
        }

        private void WritePropertyValue( XmlWriter writer, object value, string propertyName )
        {
            object propertyValue = null;
            IDictionary dictionaryValue = (IDictionary)value;

            if ( dictionaryValue.Contains( propertyName ) )
            {
                propertyValue = dictionaryValue[ propertyName ];
            }

            if ( propertyValue != null )
            {
                if ( settings.WriteType )
                {
                    writer.WriteAttributeString( "type", this.settings.TypeNameConverter.GetTypeName( propertyValue.GetType() ) );
                }

                IObjectWriter propertyWriter = this.settings.GetWriter( propertyValue.GetType() );
                propertyWriter.Write( writer, propertyValue );
            }
        }
    }
}