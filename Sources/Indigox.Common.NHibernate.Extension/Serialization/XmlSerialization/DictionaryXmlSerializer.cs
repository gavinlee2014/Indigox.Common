using System;
using System.Xml;
using Indigox.Common.NHibernate.Extension.Reflection;

namespace Indigox.Common.NHibernate.Extension.Serialization.XmlSerialization
{
    internal class DictionaryXmlSerializer : IXmlSerializer
    {
        private static DictionaryXmlSerializer instance = new DictionaryXmlSerializer();

        private DictionaryXmlSerializer()
        {
        }

        public static DictionaryXmlSerializer Instance
        {
            get { return instance; }
        }

        public object Deserialize( Type type, XmlReader reader )
        {
            object obj = Activator.CreateInstance( type );

            reader.Read();
            if ( reader.Name != "dictionary" )
            {
                throw new Exception( string.Format( "Expect dictionary node, but is {0} node.", reader.Name ) );
            }
            if ( reader.IsEmptyElement )
            {
                return obj;
            }
            while ( reader.Read() )
            {
                if ( reader.NodeType == XmlNodeType.EndElement )
                {
                    break;
                }
                if ( reader.Name == "item" )
                {
                    if ( !reader.IsEmptyElement )
                    {
                        object key = null;
                        object value = null;

                        reader.Read();
                        if ( reader.Name == "key" )
                        {
                            if ( !reader.IsEmptyElement )
                            {
                                reader.Read();
                                using ( XmlReader subxmlreader = reader.ReadSubtree() )
                                {
                                    key = XmlSerializer.Deserialize( subxmlreader );
                                }
                                ReadEndElement( reader, "key" );
                            }
                            else
                            {
                                // key = null;
                            }
                        }
                        else
                        {
                            throw new Exception( string.Format( "Expect key node, but is {0} node.", reader.Name ) );
                        }

                        reader.Read();
                        if ( reader.Name == "value" )
                        {
                            if ( !reader.IsEmptyElement )
                            {
                                reader.Read();
                                using ( XmlReader subxmlreader = reader.ReadSubtree() )
                                {
                                    value = XmlSerializer.Deserialize( subxmlreader );

                                    //Debug.WriteLine( subxmlreader.ReadInnerXml() );
                                }
                                ReadEndElement( reader, "value" );
                            }
                            else
                            {
                                // value = null;
                            }
                        }
                        else
                        {
                            throw new Exception( string.Format( "Expect value node, but is {0} node.", reader.Name ) );
                        }

                        DictionaryAccessor.Add( obj, key, value );

                        ReadEndElement( reader, "item" );
                    }
                    else
                    {
                        throw new Exception( "Item node expect key and value nodes, but is an empty node." );
                    }
                }
                else
                {
                    throw new Exception( string.Format( "Expect item node, but is {0} node.", reader.Name ) );
                }
            }
            return obj;
        }

        public void Serialize( object dictionary, XmlWriter writer )
        {
            writer.WriteStartElement( "dictionary" );

            DictionaryAccessor.ForEach( dictionary, ( key, value ) =>
            {
                writer.WriteStartElement( "item" );
                WriteDictionaryKeyXml( key, writer );
                WriteDictionaryValueXml( value, writer );
                writer.WriteEndElement(); // </item>
            } );

            writer.WriteEndElement(); // </dictionary>
        }

        private void WriteDictionaryKeyXml( object key, XmlWriter writer )
        {
            writer.WriteStartElement( "key" );
            XmlSerializer.Serialize( key, writer );
            writer.WriteEndElement(); // </key>
        }

        private void WriteDictionaryValueXml( object value, XmlWriter writer )
        {
            writer.WriteStartElement( "value" );
            XmlSerializer.Serialize( value, writer );
            writer.WriteEndElement(); // </value>
        }

        private void ReadEndElement( XmlReader xmlreader, string name )
        {
            xmlreader.Read(); // read end of value element
            if ( xmlreader.NodeType != XmlNodeType.EndElement || xmlreader.Name != name )
            {
                throw new Exception( string.Format( "Expect end of {2} element, but is an {0} node named {1}.", xmlreader.NodeType, xmlreader.Name, name ) );
            }
        }
    }
}