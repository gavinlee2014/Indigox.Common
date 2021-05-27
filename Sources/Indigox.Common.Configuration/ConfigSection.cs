using System;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Indigox.Common.Configuration
{
    public class ConfigSection : ConfigElement, IConfigurationSectionHandler
    {
        object IConfigurationSectionHandler.Create( object parent, object configContext, XmlNode section )
        {
            ConfigSection newConfig = null;
            Type type = this.GetType();

            using ( XmlReader reader = new XmlNodeReader( section ) )
            {
                XmlSerializer serializer = new XmlSerializer( type, new XmlRootAttribute( section.Name ) );
                newConfig = (ConfigSection)serializer.Deserialize( reader );
            }
            return newConfig;
        }

        public string ToXml()
        {
            XmlSerializer serializer = new XmlSerializer( this.GetType() );
            using ( StringWriter writer = new StringWriter() )
            {
                serializer.Serialize( writer, this );
                return writer.ToString();
            }
        }
    }
}