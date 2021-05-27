using System;
using System.Xml;

namespace Indigox.Common.NHibernate.Extension.Serialization.XmlSerialization
{
    internal interface IXmlSerializer
    {
        object Deserialize( Type type, XmlReader reader );

        void Serialize( object value, XmlWriter writer );
    }
}