using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Indigox.Common.NHibernate.Extension.Serialization
{
    public class AnyTypeXmlSerializer
    {
        public static string Serialize( object value )
        {
            if ( value == null )
            {
                return null;
            }

            StringBuilder xmlBuilder = new StringBuilder();
            using ( TextWriter txtWriter = new StringWriter( xmlBuilder ) )
            using ( XmlWriter xmlWriter = new IgnoreDeclarationXmlWriter( txtWriter ) )
            {
                XmlSerializer.Serialize( value, xmlWriter );
            }
            return xmlBuilder.ToString();
        }

        public static object Deserialize( string text )
        {
            if ( string.IsNullOrEmpty( text ) )
            {
                return null;
            }

            object obj = null;
            using ( TextReader txtReader = new StringReader( text ) )
            using ( XmlReader xmlReader = new IngoreWhitespaceXmlReader( txtReader ) )
            {
                obj = XmlSerializer.Deserialize( xmlReader );
            }
            return obj;
        }
    }
}