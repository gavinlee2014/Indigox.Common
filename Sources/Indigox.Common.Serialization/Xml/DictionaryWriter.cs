using System;
using System.Collections;
using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    internal class DictionaryWriter : IObjectWriter
    {
        public DictionaryWriter( WriterSettings settings )
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
            IDictionary dictionaryValue = (IDictionary)value;

            writer.WriteStartElement( "dictionary" );
            {
                if ( settings.WriteType )
                {
                    writer.WriteAttributeString( "type", this.settings.TypeNameConverter.GetTypeName( type ) );
                }

                foreach ( string key in dictionaryValue.Keys )
                {
                    IPropertyWriter keyValueWriter = new DictionaryItemWriter( this.settings );
                    keyValueWriter.Write( writer, value, key );
                }
            }
            writer.WriteEndElement();
        }
    }
}