using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Indigox.Common.Configuration.Test.Configs
{
    public class OrganizationConfigSection : ConfigSection
    {
        public OrganizationConfigSection()
        {
            // init defaults
            this.Users = new List<UserElement>();
        }

        [XmlElement( "boss" )]
        public UserElement Boss { get; set; }

        [XmlElement( "userBoss", typeof( UserElement ) )]
        [XmlElement( "managerBoss", typeof( ManagerElement ) )]
        public UserElement BigBoss { get; set; }

        [XmlElement( "departBoss" )]
        public UserElement DepartBoss { get; set; }

        [XmlArray( "users" )]
        [XmlArrayItem( "user", typeof( UserElement ) )]
        [XmlArrayItem( "manager", typeof( ManagerElement ) )]
        public List<UserElement> Users { get; set; }

        [XmlElement( "ext" )]
        public ExtendPropertiesElement Extends { get; set; }

        //public static OrganizationConfigSection LoadFromConfig(string path)
        //{
        //    OrganizationConfigSection section = (OrganizationConfigSection)ConfigurationManager.GetSection(path);
        //    return section;
        //}

        //public static OrganizationConfigSection LoadFromXmlFile(string filename)
        //{
        //    OrganizationConfigSection section = null;
        //    using (StreamReader reader = new StreamReader(filename))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(OrganizationConfigSection), new XmlRootAttribute("config"));
        //        section = (OrganizationConfigSection)serializer.Deserialize(reader);
        //    }
        //    return section;
        //}

        public OrganizationConfigSection SetBoss( string name, string desc )
        {
            this.Boss = new ManagerElement( name, desc );
            return this;
        }

        public OrganizationConfigSection SetDepartBoss( string name )
        {
            this.DepartBoss = new UserElement( name );
            return this;
        }

        public OrganizationConfigSection AddUser( string name )
        {
            this.Users.Add( new UserElement( name ) );
            return this;
        }
    }
}