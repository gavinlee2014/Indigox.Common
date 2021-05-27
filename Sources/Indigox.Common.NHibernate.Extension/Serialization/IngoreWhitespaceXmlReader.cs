using System;
using System.IO;
using System.Xml;

namespace Indigox.Common.NHibernate.Extension.Serialization
{
    internal class IngoreWhitespaceXmlReader : XmlTextReader
    {
        public IngoreWhitespaceXmlReader( TextReader reader )
            : base( reader )
        {
            this.WhitespaceHandling = WhitespaceHandling.Significant;
        }
    }
}