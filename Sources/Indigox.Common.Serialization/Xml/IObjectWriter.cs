using System.Xml;

namespace Indigox.Common.Serialization.Xml
{
    internal interface IObjectWriter
    {
        WriterSettings Settings { get; }
        void Write( XmlWriter writer, object value );
    }
}
