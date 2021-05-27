using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate.Type;

namespace Indigox.Common.NHibernate.Extension.Serialization
{
    internal class BinarySerializer
    {
        public static byte[] Serialize( object obj )
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                bf.Serialize( stream, obj );
                return stream.ToArray();
            }
            catch ( Exception e )
            {
                throw new SerializationException( "Could not serialize a serializable property: ", e );
            }
        }

        public static object Deserialize( byte[] bytes )
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize( new MemoryStream( bytes ) );
            }
            catch ( Exception e )
            {
                throw new SerializationException( "Could not deserialize a serializable property: ", e );
            }
        }
    }
}