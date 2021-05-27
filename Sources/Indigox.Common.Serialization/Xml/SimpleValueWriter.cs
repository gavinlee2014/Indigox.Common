using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    internal class SimpleValueWriter : IObjectWriter
    {
        public SimpleValueWriter( WriterSettings settings )
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
            if ( value != null )
            {
                writer.WriteString( value.ToString() );
            }
        }
    }
}