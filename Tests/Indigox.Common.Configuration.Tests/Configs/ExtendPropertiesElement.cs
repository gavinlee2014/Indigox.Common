using System;
using System.Xml.Serialization;

namespace Indigox.Common.Configuration.Test.Configs
{
    public class ExtendPropertiesElement : ConfigElement
    {
        [XmlAttribute( "intVal" )]
        public int IntVal { get; set; }

        [XmlAttribute( "shortVal" )]
        public short ShortVal { get; set; }

        [XmlAttribute( "longVal" )]
        public long LongVal { get; set; }

        [XmlAttribute( "boolVal" )]
        public bool BoolVal { get; set; }

        [XmlAttribute( "doubleVal" )]
        public double DoubleVal { get; set; }

        [XmlAttribute( "floatVal" )]
        public float FloatVal { get; set; }

        [XmlAttribute( "byteVal" )]
        public byte ByteVal { get; set; }

        [XmlAttribute( "bytesVal" )]
        public byte[] BytesVal { get; set; }

        [XmlAttribute( "dateTimeVal" )]
        public DateTime DateTimeVal { get; set; }

        [XmlAttribute( "guidVal" )]
        public Guid GuidVal { get; set; }

        [XmlAttribute( "stringVal" )]
        public string StringVal { get; set; }

        [XmlAttribute( "charVal" )]
        public char CharVal { get; set; }

        [XmlAttribute( "enumStateVal" )]
        public State EnumStateVal { get; set; }
    }

    public enum State
    {
        Large,
        Middle,
        Small,
    }
}