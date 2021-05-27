using System;

namespace Indigox.TestUtility.Builders
{
    class PrimativeTypes
    {
        public static bool IsNumber( object value )
        {
            return ( ( value is int ) ||
                     ( value is long ) ||
                     ( value is short ) ||
                     ( value is uint ) ||
                     ( value is ulong ) ||
                     ( value is ushort ) ||
                     ( value is decimal ) ||
                     ( value is double ) ||
                     ( value is float ) );
        }
        public static bool IsBoolean( object value )
        {
            return ( value is bool );
        }
        public static bool IsDateTime( object value )
        {
            return ( value is DateTime );
        }
        public static bool IsString( object value )
        {
            return ( value is string );
        }
    }
}
