using System;
using System.Collections;
using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    internal class CollectionWriter : IObjectWriter
    {
        public CollectionWriter( WriterSettings settings )
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
            IEnumerable enumerable = (IEnumerable)value;

            writer.WriteStartElement( "collection" );
            {
                if ( settings.WriteType )
                {
                    writer.WriteAttributeString( "type", this.settings.TypeNameConverter.GetTypeName( type ) );
                }

                foreach ( object item in enumerable )
                {
                    writer.WriteStartElement( "item" );
                    {
                        if ( item != null )
                        {
                            Type itemType = item.GetType();
                            if ( settings.WriteType )
                            {
                                writer.WriteAttributeString( "type", this.settings.TypeNameConverter.GetTypeName( itemType ) );
                            }
                            this.settings.GetWriter( itemType ).Write( writer, item );
                        }
                    }
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
    }
}