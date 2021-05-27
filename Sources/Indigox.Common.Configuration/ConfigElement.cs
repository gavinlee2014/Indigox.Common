using System;
using System.Xml.Serialization;

namespace Indigox.Common.Configuration
{
    public class ConfigElement
    {
        [XmlAttribute( "xml:base" )]
        protected string xmlbase;
    }
}