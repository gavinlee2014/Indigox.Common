using System;
using Newtonsoft.Json;

namespace Indigox.Common.Serialization
{
    public class JsonSerializer : ISerializer
    {
        private SerializerSettings settings;

        public JsonSerializer()
            : this( new SerializerSettings() )
        {

        }

        public JsonSerializer( SerializerSettings settings )
        {
            this.settings = settings;
        }

        public object Deserialize( string json )
        {
            throw new NotSupportedException( "json serializer must specify an type, see Deserialize( Type type, string json )" );
        }

        public object Deserialize( Type type, string json )
        {
            return JsonConvert.DeserializeObject( json, type );
        }

        public T Deserialize<T>( string json )
        {
            return (T)Deserialize( typeof( T ), json );
        }

        public string Serialize( object value )
        {
            Formatting fmt = ( this.settings.Indent ) ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject( value, fmt );
        }
    }
}