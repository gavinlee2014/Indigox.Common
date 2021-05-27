using System;
using System.Collections.Generic;

namespace Indigox.Common.Configuration
{
    public class PrimativeTypes
    {
        static PrimativeTypes()
        {
            Initialize();
        }

        private static void Initialize()
        {
            Register( typeof( int ) );
            Register( typeof( short ) );
            Register( typeof( long ) );
            Register( typeof( uint ) );
            Register( typeof( ushort ) );
            Register( typeof( ulong ) );
            Register( typeof( float ) );
            Register( typeof( double ) );
            Register( typeof( decimal ) );
            Register( typeof( bool ) );
            Register( typeof( DateTime ) );
            Register( typeof( byte ) );
            Register( typeof( byte[] ) );
            Register( typeof( Guid ) );
            Register( typeof( char ) );
            Register( typeof( string ) );
        }

        private static Dictionary<Type, IConvert> registerTypes = new Dictionary<Type, IConvert>();

        private static void Register( Type type )
        {
            AssertArgumentNull( type, "type" );
            Register( type, new DefaultConvert() );
        }

        private static void Register( Type type, IConvert convert )
        {
            AssertArgumentNull( type, "type" );
            AssertArgumentNull( convert, "convert" );

            if ( !registerTypes.ContainsKey( type ) )
            {
                registerTypes.Add( type, convert );
            }
        }

        public static bool IsPrimativeType( Type type )
        {
            AssertArgumentNull( type, "type" );

            if ( ClassHelper.IsGenericTypeOf( type, typeof( Nullable<> ) ) )
            {
                type = ClassHelper.GetGenericArgumentType( type, 0 );
            }
            if ( type.IsEnum )
            {
                return true;
            }
            return registerTypes.ContainsKey( type );
        }

        public static bool IsPrimativeObject( object value )
        {
            if ( value == null ) return true;
            return IsPrimativeType( value.GetType() );
        }

        public static string ConvertToString( object value )
        {
            AssertPrimativeObject( value );

            if ( value == null )
            {
                return null;
            }
            Type type = value.GetType();
            return ConvertToString( type, value );
        }

        public static string ConvertToString( Type type, object value )
        {
            AssertPrimativeObject( value );

            Type inputedType = type;
            Type registedType = type;
            if ( ClassHelper.IsGenericTypeOf( type, typeof( Nullable<> ) ) )
            {
                registedType = ClassHelper.GetGenericArgumentType( type, 0 );
            }
            if ( registedType.IsEnum )
            {
                if ( value == null )
                    return null;
                return value.ToString();
            }
            return registerTypes[ registedType ].ConvertToString( inputedType, value );
        }

        public static object ConvertFromString( Type type, string text )
        {
            AssertArgumentNull( type, "type" );
            AssertPrimativeType( type );

            Type inputedType = type;
            Type registedType = type;
            if ( ClassHelper.IsGenericTypeOf( type, typeof( Nullable<> ) ) )
            {
                registedType = ClassHelper.GetGenericArgumentType( type, 0 );
            }
            if ( registedType.IsEnum )
            {
                if ( string.IsNullOrEmpty( text ) )
                    return null;
                return Enum.Parse( registedType, text );
            }
            return registerTypes[ registedType ].ConvertFromString( inputedType, text );
        }

        private static void AssertArgumentNull( object arg, string argName )
        {
            if ( arg == null )
            {
                throw new ArgumentNullException( argName );
            }
        }

        private static void AssertPrimativeType( Type type )
        {
            if ( !IsPrimativeType( type ) )
            {
                throw new ArgumentException( "type '" + ClassHelper.GetTypeName( type ) + "' is not primative type." );
            }
        }

        private static void AssertPrimativeObject( object value )
        {
            if ( !IsPrimativeObject( value ) )
            {
                throw new ArgumentException( "object is not primative object, object's type is '" + ClassHelper.GetTypeName( value.GetType() ) + "'." );
            }
        }
    }
}