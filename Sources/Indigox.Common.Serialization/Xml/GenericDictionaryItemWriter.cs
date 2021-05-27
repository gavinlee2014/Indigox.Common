using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    internal class GenericDictionaryItemWriter : IPropertyWriter
    {
        public GenericDictionaryItemWriter( WriterSettings settings )
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
            Type type = value.GetType();
            Type interfaceType = typeof( IDictionary<,> ).MakeGenericType( type.GetGenericArguments() );

            MethodInfo containsMethod =  interfaceType.GetMethod( "ContainsKey" );
            PropertyInfo index = interfaceType.GetProperty( "Item" );
            if ( (bool)containsMethod.Invoke( value, new object[] { propertyName } ) )
            {
                propertyValue = index.GetValue( value, new object[] { propertyName } );
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