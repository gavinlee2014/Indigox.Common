using System.Xml.Serialization;

namespace Indigox.Common.Configuration.Test.Configs
{
    public class UserElement : ConfigElement
    {
        public UserElement()
        {
        }

        public UserElement( string name )
        {
            this.Name = name;
        }

        [XmlAttribute( "name" )]
        public string Name { get; set; }
    }
}