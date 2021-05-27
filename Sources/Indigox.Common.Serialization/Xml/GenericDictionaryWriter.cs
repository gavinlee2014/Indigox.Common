using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    internal class GenericDictionaryWriter : IObjectWriter
    {
        public GenericDictionaryWriter( WriterSettings settings )
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
            PropertyInfo keyProperty = typeof( KeyValuePair<,> ).MakeGenericType( type.GetGenericArguments() ).GetProperty( "Key" );

            writer.WriteStartElement( "dictionary" );
            {
                if ( settings.WriteType )
                {
                    writer.WriteAttributeString( "type", this.settings.TypeNameConverter.GetTypeName( type ) );
                }

                foreach ( object keyValuePair in enumerable )
                {
                    string key = (string)keyProperty.GetValue( keyValuePair, null );
                    IPropertyWriter keyValueWriter = new GenericDictionaryItemWriter( this.settings );
                    keyValueWriter.Write( writer, value, key );
                }
            }
            writer.WriteEndElement();
        }
    }
}