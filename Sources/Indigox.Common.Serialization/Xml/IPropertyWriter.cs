using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    interface IPropertyWriter
    {
        WriterSettings Settings { get; }
        void Write( XmlWriter writer, object value, string propertyName );
    }
}
