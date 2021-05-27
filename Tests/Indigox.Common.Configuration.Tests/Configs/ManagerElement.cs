using System.Xml.Serialization;

namespace Indigox.Common.Configuration.Test.Configs
{
    public class ManagerElement : UserElement
    {
        public ManagerElement()
        {
        }

        public ManagerElement( string name, string desc )
            : base( name )
        {
            this.Description = desc;
        }

        [XmlAttribute( "desc" )]
        public string Description { get; set; }
    }
}