using System;
using System.Xml;
using Indigox.Common.NHibernate.Extension.Reflection;

namespace Indigox.Common.NHibernate.Extension.Serialization.XmlSerialization
{
    internal class CollectionXmlSerializer : IXmlSerializer
    {
        private static CollectionXmlSerializer instance = new CollectionXmlSerializer();

        private CollectionXmlSerializer()
        {
        }

        public static CollectionXmlSerializer Instance
        {
            get { return instance; }
        }

        public object Deserialize( Type type, XmlReader reader )
        {
            object obj = Activator.CreateInstance( type );

            reader.Read();
            if ( reader.Name != "collection" )
            {
                throw new Exception( string.Format( "Expect collection node, but is {0} node.", reader.Name ) );
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
                        if ( reader.NodeType != XmlNodeType.EndElement )
                        {
                            reader.Read();
                            using ( XmlReader subxmlreader = reader.ReadSubtree() )
                            {
                                object item = XmlSerializer.Deserialize( subxmlreader );
                                CollectionAccessor.Add( obj, item );
                            }
                        }
                        else
                        {
                            CollectionAccessor.Add( obj, null );
                        }
                        ReadEndElement( reader, "item" );
                    }
                    else
                    {
                        CollectionAccessor.Add( obj, null );
                    }
                }
                else
                {
                    throw new Exception( string.Format( "Expect item node, but is {0} node.", reader.Name ) );
                }
            }
            return obj;
        }

        public void Serialize( object collection, XmlWriter writer )
        {
            writer.WriteStartElement( "collection" );

            CollectionAccessor.ForEach( collection, ( item, i ) =>
            {
                writer.WriteStartElement( "item" );
                XmlSerializer.Serialize( item, writer );
                writer.WriteEndElement(); // </item>
            } );

            writer.WriteEndElement(); // </collection>
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