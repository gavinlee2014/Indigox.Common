using System;

namespace Indigox.Common.Serialization
{
    interface ISerializer
    {
        object Deserialize( string text );
        object Deserialize( Type type, string text );
        T Deserialize<T>( string text );
        string Serialize( object value );
    }
}
