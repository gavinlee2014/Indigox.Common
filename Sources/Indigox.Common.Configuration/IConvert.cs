using System;

namespace Indigox.Common.Configuration
{
    public interface IConvert
    {
        object ConvertFromString( Type type, string text );

        string ConvertToString( Type type, object value );
    }
}