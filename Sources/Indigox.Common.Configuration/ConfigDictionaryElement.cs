using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Indigox.Common.Logging;

namespace Indigox.Common.Configuration
{
    public class ConfigDictionaryElement<T> : ConfigElement, IDictionary<string, T>, IXmlSerializable
    {
        private Dictionary<string, T> innerDictionary;

        public ConfigDictionaryElement()
        {
            innerDictionary = new Dictionary<string, T>();
        }

        public ConfigDictionaryElement( StringComparer comparer )
        {
            innerDictionary = new Dictionary<string, T>( comparer );
        }

        public ConfigDictionaryElement( IDictionary<string, T> initDictionary )
        {
            innerDictionary = new Dictionary<string, T>( initDictionary );
        }

        public ConfigDictionaryElement( IDictionary<string, T> initDictionary, StringComparer comparer )
        {
            innerDictionary = new Dictionary<string, T>( initDictionary, comparer );
        }

        protected virtual XmlSchema GetSchema()
        {
            return null;
        }

        protected virtual void ReadXml( XmlReader reader )
        {
            Debug.WriteLine( reader.Name );
            ReadToFirstItemElement( reader );
            do
            {
                if ( reader.NodeType == XmlNodeType.Element )
                {
                    string elementName = reader.Name;
                    string typeName = reader.GetAttribute( "type" );
                    string isNull = reader.GetAttribute( "isnull" );
                    if ( isNull == "true" )
                    {
                        this.innerDictionary.Add( elementName, default( T ) );
                    }
                    else
                    {
                        Type type = ( TypeAlias.Contains( typeName ) ) ? TypeAlias.GetType( typeName ) : Type.GetType( typeName );
                        if ( PrimativeTypes.IsPrimativeType( type ) )
                        {
                            //ReadToFirstItemElement( reader );
                            string text = reader.ReadElementContentAsString();
                            this.innerDictionary.Add( elementName, (T)PrimativeTypes.ConvertFromString( type, text ) );
                        }
                        else
                        {
                            ReadToFirstItemElement( reader );
                            using ( XmlReader subReader = reader.ReadSubtree() )
                            {
                                XmlSerializer serizlizer = new XmlSerializer( type );
                                object val = serizlizer.Deserialize( subReader );
                                this.innerDictionary.Add( elementName, (T)val );
                            }
                        }
                    }
                }
            } while ( ReadToNextItemElement( reader ) );
        }

        protected virtual void WriteXml( XmlWriter writer )
        {
            foreach ( var item in this.innerDictionary )
            {
                writer.WriteStartElement( item.Key );
                if ( item.Value == null )
                {
                    writer.WriteAttributeString( "isnull", "true" );
                }
                else
                {
                    Type type = item.Value.GetType();
                    writer.WriteAttributeString( "type", ( TypeAlias.HasAlias( type ) ) ? TypeAlias.GetAlias( type ) : type.AssemblyQualifiedName );
                    if ( PrimativeTypes.IsPrimativeType( type ) )
                    {
                        writer.WriteString( PrimativeTypes.ConvertToString( type, item.Value ) );
                    }
                    else
                    {
                        XmlSerializer serizlizer = new XmlSerializer( type );
                        serizlizer.Serialize( writer, item.Value );
                    }
                }
                writer.WriteEndElement();
            }
        }

        private static bool ReadToNextItemElement( XmlReader reader )
        {
            bool ret = reader.Read();
            Log.Debug( reader.NodeType + ", " + reader.Name );
            while ( reader.NodeType != XmlNodeType.Element && ret )
            {
                ret = reader.Read();
                Log.Debug( reader.NodeType + ", " + reader.Name );
            }
            return ret;
        }

        private static bool ReadToFirstItemElement( XmlReader reader )
        {
            bool ret = reader.Read();
            Log.Debug( reader.NodeType + ", " + reader.Name );
            while ( reader.NodeType != XmlNodeType.Element && ret )
            {
                ret = reader.Read();
                Log.Debug( reader.NodeType + ", " + reader.Name );
            }
            return ret;
        }

        #region implement interface IDictionary<string, T>

        public void Add( string key, T value )
        {
            this.innerDictionary.Add( key, value );
        }

        public bool ContainsKey( string key )
        {
            return this.innerDictionary.ContainsKey( key );
        }

        public ICollection<string> Keys
        {
            get { return this.innerDictionary.Keys; }
        }

        public bool Remove( string key )
        {
            return this.innerDictionary.Remove( key );
        }

        public bool TryGetValue( string key, out T value )
        {
            return this.innerDictionary.TryGetValue( key, out value );
        }

        public ICollection<T> Values
        {
            get { return this.innerDictionary.Values; }
        }

        public T this[ string key ]
        {
            get
            {
                return this.innerDictionary[ key ];
            }
            set
            {
                this.innerDictionary[ key ] = value;
            }
        }

        public void Clear()
        {
            this.innerDictionary.Clear();
        }

        public int Count
        {
            get { return this.innerDictionary.Count; }
        }

        void ICollection<KeyValuePair<string, T>>.Add( KeyValuePair<string, T> item )
        {
            ( (IDictionary<string, T>)this.innerDictionary ).Add( item );
        }

        bool ICollection<KeyValuePair<string, T>>.Contains( KeyValuePair<string, T> item )
        {
            return ( (IDictionary<string, T>)this.innerDictionary ).Contains( item );
        }

        void ICollection<KeyValuePair<string, T>>.CopyTo( KeyValuePair<string, T>[] array, int arrayIndex )
        {
            ( (IDictionary<string, T>)this.innerDictionary ).CopyTo( array, arrayIndex );
        }

        bool ICollection<KeyValuePair<string, T>>.IsReadOnly
        {
            get { return ( (IDictionary<string, T>)this.innerDictionary ).IsReadOnly; }
        }

        bool ICollection<KeyValuePair<string, T>>.Remove( KeyValuePair<string, T> item )
        {
            return ( (IDictionary<string, T>)this.innerDictionary ).Remove( item );
        }

        IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator()
        {
            return this.innerDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.innerDictionary.GetEnumerator();
        }

        #endregion implement interface IDictionary<string, T>

        #region implement interface IXmlSerializable

        XmlSchema IXmlSerializable.GetSchema()
        {
            return this.GetSchema();
        }

        void IXmlSerializable.ReadXml( XmlReader reader )
        {
            this.ReadXml( reader );
        }

        void IXmlSerializable.WriteXml( XmlWriter writer )
        {
            this.WriteXml( writer );
        }

        #endregion implement interface IXmlSerializable
    }
}