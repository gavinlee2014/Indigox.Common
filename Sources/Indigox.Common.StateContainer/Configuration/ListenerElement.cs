using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Indigox.Common.Configuration;

namespace Indigox.Common.StateContainer.Configuration
{
    public class ListenerElement : ConfigElement
    {
        private string typeName;

        [XmlAttribute( "type" )]
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
    }
}
