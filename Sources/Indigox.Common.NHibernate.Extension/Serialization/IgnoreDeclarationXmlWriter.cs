using System;
using System.IO;
using System.Xml;

namespace Indigox.Common.NHibernate.Extension.Serialization
{
    internal class IgnoreDeclarationXmlWriter : XmlTextWriter
    {
        public IgnoreDeclarationXmlWriter( TextWriter writer )
            : base( writer )
        {
            this.Formatting = System.Xml.Formatting.Indented;
        }
    }
}