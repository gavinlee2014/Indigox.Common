using System;
using System.Collections;
using System.Diagnostics;

namespace Indigox.Common.Utilities
{
    public class ArgumentAssert
    {

        [DebuggerHidden]
        public static void NotNull( object value, string argName )
        {
            if ( value == null )
            {
                throw new ArgumentException( string.Format( "参数{0}不允许为null", argName ) );
            }
        }

        [DebuggerHidden]
        public static void NotEmpty( ICollection value, string argName )
        {
            if ( value.Count == 0 )
            {
                throw new ArgumentException( string.Format( "参数{0}不允许为空集合", argName ) );
            }
        }

        [DebuggerHidden]
        public static void NotEmpty( IEnumerable value, string argName )
        {
            if ( value == null || !value.GetEnumerator().MoveNext() )
            {
                throw new ArgumentException( string.Format( "参数{0}不允许为空集合", argName ) );
            }
        }

        [DebuggerHidden]
        public static void NotEmpty( Guid value, string argName )
        {
            if ( value == Guid.Empty )
            {
                throw new ArgumentException( string.Format( "参数{0}不允许为空", argName ) );
            }
        }

        [DebuggerHidden]
        public static void NotEmpty( string value, string argName )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( string.Format( "参数{0}不允许空", argName ) );
            }
        }

        [DebuggerHidden]
        public static void NotContains( string keyword, string value, string argName )
        {
            if ( value.Contains( keyword ) )
            {
                throw new ArgumentException( string.Format( "参数{0}不允许包含{1}", argName, keyword ) );
            }
        }

        [DebuggerHidden]
        public static void IsTypeOf( Type type, object value, string argName )
        {
            if ( value == null )
            {
                // null can assign to all type
                return;
            }
            Type valueType = value.GetType();
            if ( !type.IsAssignableFrom( valueType ) )
            {
                throw new ArgumentException( string.Format( "参数{0}的类型是{2}，与需要的参数类型{1}不匹配", 
                    argName, type.FullName, valueType.FullName ) );
            }
        }

        [DebuggerHidden]
        public static void IsTrue( bool value, string message )
        {
            if ( !value )
            {
                throw new ArgumentException( message );
            }
        }

        [DebuggerHidden]
        public static void Fault( string message )
        {
            throw new ArgumentException( message );
        }
    }
}
