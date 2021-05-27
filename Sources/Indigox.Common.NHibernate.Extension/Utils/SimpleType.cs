using System;

namespace Indigox.Common.NHibernate.Extension.Utils
{
    internal class SimpleType
    {
        private static SimpleType instance = new SimpleType();

        public static SimpleType Instance
        {
            get { return instance; }
        }

        public bool IsSimpleType( Type type )
        {
            if ( type == typeof( Int16 ) ||
                 type == typeof( Int32 ) ||
                 type == typeof( Int64 ) ||
                 type == typeof( UInt16 ) ||
                 type == typeof( UInt32 ) ||
                 type == typeof( UInt64 ) ||
                 type == typeof( String ) ||
                 type == typeof( Boolean ) ||
                 type == typeof( Double ) ||
                 type == typeof( Single ) ||
                 type == typeof( Decimal ) ||
                 type == typeof( Guid ) ||
                 type == typeof( DateTime ) ||
                 type.IsEnum )
            {
                return true;
            }
            return false;
        }

        public string ConvertToString( object value )
        {
            if ( value == null )
            {
                return null;
            }

            Type type = value.GetType();

            if ( !IsSimpleType( type ) )
            {
                throw new Exception( "Value is not simple type value." );
            }

            if ( type.IsEnum )
            {
                return Enum.Format( type, value, "d" );
            }

            return value.ToString();
        }

        public object ConvertFromString( string str, Type type )
        {
            if ( !IsSimpleType( type ) )
            {
                throw new Exception( "Value is not simple type value." );
            }

            if ( type == typeof( String ) )
            {
                return str;
            }

            if ( type == typeof( Int16 ) ||
                 type == typeof( Int32 ) ||
                 type == typeof( Int64 ) ||
                 type == typeof( UInt16 ) ||
                 type == typeof( UInt32 ) ||
                 type == typeof( UInt64 ) ||
                 type == typeof( Boolean ) ||
                 type == typeof( Double ) ||
                 type == typeof( Single ) ||
                 type == typeof( Decimal ) )
            {
                return Convert.ChangeType( str, type );
            }

            if ( type == typeof( Guid ) )
            {
                return new Guid( str );
            }

            if ( type == typeof( DateTime ) )
            {
                return DateTime.Parse( str );
            }

            if ( type.IsEnum )
            {
                int intVal = int.Parse( str );
                return Enum.ToObject( type, intVal );
            }

            throw new Exception( "Can't convert from string to " + type.FullName + "." );
        }
    }
}