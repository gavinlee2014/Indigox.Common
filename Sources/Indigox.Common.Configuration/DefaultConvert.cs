using System;
using System.Text;

namespace Indigox.Common.Configuration
{
    internal class DefaultConvert : IConvert
    {
        public string ConvertToString( Type type, object value )
        {
            if ( value == null )
            {
                return null;
            }
            if ( value is string )
            {
                return (string)value;
            }
            if ( value is byte || value is Nullable<byte> )
            {
                byte b = (byte)value;
                return "0x" + b.ToString( "X" );
            }
            if ( value is byte[] )
            {
                return GetBytesHexString( value );
            }
            if ( value is Guid )
            {
                return ( (Guid)value ).ToString( "B" );
            }
            if ( value is DateTime )
            {
                return ( (DateTime)value ).ToString( "yyyy/MM/dd HH:mm:ss" );
            }
            return value.ToString();
        }

        public object ConvertFromString( Type type, string text )
        {
            if ( type == typeof( string ) )
            {
                return text;
            }
            if ( type == typeof( byte ) && text.StartsWith( "0x" ) )
            {
                if ( text.Length == 3 )
                {
                    return GetByteFromHex( '0', text[ 2 ] );
                }
                if ( text.Length == 4 )
                {
                    return GetByteFromHex( text[ 2 ], text[ 2 ] );
                }
            }
            if ( type == typeof( byte[] ) )
            {
                return GetBytesFromString( text );
            }
            if ( type == typeof( Guid ) )
            {
                return new Guid( text );
            }
            if ( type == typeof( DateTime ) )
            {
                return DateTime.Parse( text );
            }
            if ( ClassHelper.IsGenericTypeOf( type, typeof( Nullable<> ) ) )
            {
                if ( string.IsNullOrEmpty( text ) )
                {
                    return null;
                }
                else
                {
                    Type genericArgumentType = ClassHelper.GetGenericArgumentType( type, 0 );
                    return ConvertFromString( genericArgumentType, text );
                }
            }
            return Convert.ChangeType( text, type );
        }

        private string GetBytesHexString( object value )
        {
            byte[] bytes = (byte[])value;
            StringBuilder strBulider = new StringBuilder( bytes.Length * 2 + 2 );
            strBulider.Append( "0x" );
            for ( int i = 0; i < bytes.Length; i++ )
            {
                strBulider.Append( bytes[ i ].ToString( "X" ).PadLeft( 2, '0' ) );
            }
            return strBulider.ToString();
        }

        private object GetBytesFromString( string text )
        {
            if ( text.StartsWith( "0x" ) )
            {
                return GetBytesFromHexString( text );
            }
            else
            {
                byte[] bs = Convert.FromBase64String( text );
                return bs;
            }
        }

        private object GetBytesFromHexString( string text )
        {
            if ( text.Length % 2 != 0 )
            {
                throw new Exception( "error hex string, the string contains odd chars." );
            }
            byte[] bs = new byte[ text.Length / 2 - 1 ];
            for ( int i = 0; i < bs.Length; i++ )
            {
                bs[ i ] = GetByteFromHex( text[ ( i + 1 ) * 2 ], text[ ( i + 1 ) * 2 + 1 ] );
            }
            return bs;
        }

        private byte GetByteFromHex( char hexHigh, char hexLow )
        {
            byte b = (byte)( GetIntFromHexChar( hexHigh ) << 4 | GetIntFromHexChar( hexLow ) );
            return b;
        }

        private int GetIntFromHexChar( char hex )
        {
            if ( hex >= '0' && hex <= '9' )
            {
                return ( hex - '0' );
            }
            if ( hex >= 'a' && hex <= 'f' )
            {
                return ( hex - 'a' + 10 );
            }
            if ( hex >= 'A' && hex <= 'F' )
            {
                return ( hex - 'A' + 10 );
            }
            throw new Exception( "error char of hex." );
        }
    }
}