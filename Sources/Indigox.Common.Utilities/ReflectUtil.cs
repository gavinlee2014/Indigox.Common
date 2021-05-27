using System;
using System.Reflection;
using Indigox.Common.Logging;

namespace Indigox.Common.Utilities
{
    public class ReflectUtil
    {
        #region reflect type

        public static Type GetType( string typeName )
        {
            AssertArgumentNull( typeName, "typeName" );
            Type type = Type.GetType( typeName, true );
            return type;
        }

        public static Type GetType( string typeName, Type baseType )
        {
            AssertArgumentNull( typeName, "typeName" );
            Type type = Type.GetType( typeName, true );
            AssertIsSubClassOf( type, baseType );
            return type;
        }

        #endregion reflect type

        #region create instance

        public static object CreateInstance( string typeName )
        {
            AssertArgumentNull( typeName, "typeName" );
            Type type = Type.GetType( typeName, true );
            object[] args = new object[] { };
            object ret = CreateInstance( type, args );
            return ret;
        }

        public static object CreateInstance( string typeName, object[] args )
        {
            AssertArgumentNull( typeName, "typeName" );
            Type type = Type.GetType( typeName, true );
            object ret = CreateInstance( type, args );
            return ret;
        }

        public static object CreateInstance( string typeName, object[] args, bool nonPublic )
        {
            AssertArgumentNull( typeName, "typeName" );
            Type type = Type.GetType( typeName, true );
            object ret = CreateInstance( type, args, nonPublic );
            return ret;
        }

        public static object CreateInstance( Type type )
        {
            AssertArgumentNull( type, "type" );
            object[] args = new object[] { };
            object ret = CreateInstance( type, args, false );
            return ret;
        }

        public static object CreateInstance( Type type, object[] args )
        {
            AssertArgumentNull( type, "type" );
            object ret = CreateInstance( type, args, false );
            return ret;
        }

        public static object CreateInstance( Type type, object[] args, bool nonPublic )
        {
            AssertArgumentNull( type, "type" );
            BindingFlags flag = nonPublic ? BindingFlags_Instance : BindingFlags_Public;
            object ret = Activator.CreateInstance( type, flag, null, args, null );
            return ret;
        }

        public static T CreateInstance<T>()
        {
            Type type = typeof( T );
            object[] args = new object[] { };
            object ret = CreateInstance( type, args );
            return (T)ret;
        }

        public static T CreateInstance<T>( object[] args )
        {
            Type type = typeof( T );
            object ret = CreateInstance( type, args );
            return (T)ret;
        }

        public static T CreateInstance<T>( object[] args, bool nonPublic )
        {
            Type type = typeof( T );
            object ret = CreateInstance( typeof( T ), args, nonPublic );
            return (T)ret;
        }

        #endregion create instance

        #region reflect method

        public static MethodInfo GetMethod( string typeName, string methodName )
        {
            Type type = GetType( typeName );
            return GetMethod( type, methodName, null, BindingFlags_Public );
        }

        public static MethodInfo GetMethod( Type type, string methodName )
        {
            return GetMethod( type, methodName, null, BindingFlags_Public );
        }

        public static MethodInfo GetMethod( Type type, string methodName, Type[] argTypes )
        {
            return GetMethod( type, methodName, argTypes, BindingFlags_Public );
        }

        public static MethodInfo GetMethod( Type type, string methodName, Type[] argTypes, bool nonPublic, bool instance )
        {
            BindingFlags bindingFlag = BindingFlags_PublicStatic;
            if ( nonPublic )
                bindingFlag |= BindingFlags.NonPublic;
            if ( instance )
                bindingFlag |= BindingFlags.Instance;
            return GetMethod( type, methodName, argTypes, bindingFlag );
        }

        public static MethodInfo GetMethod( Type type, string methodName, Type[] argTypes, BindingFlags bindingFlag )
        {
            if ( argTypes != null )
            {
                return type.GetMethod( methodName, bindingFlag, null, argTypes, null );
            }
            else
            {
                return type.GetMethod( methodName, bindingFlag );
            }
        }

        #endregion reflect method

        #region invoke method

        public static object InvokeMethod( object instance, string methodName, Type[] argTypes, object[] args )
        {
            AssertArgumentNull( instance, "instance" );
            AssertArgumentNull( methodName, "methodName" );
            AssertArgumentNull( argTypes, "argTypes" );
            AssertArgumentNull( args, "args" );

            Log.Debug( String.Format( "Invoke method {1} from {0}", instance.GetType().FullName, methodName ) );

            MethodInfo method = GetMethod( instance.GetType(), methodName, argTypes, BindingFlags_Instance );
            return method.Invoke( instance, args );
        }

        public static object InvokeStaticMethod( Type type, string methodName, Type[] argTypes, object[] args )
        {
            AssertArgumentNull( type, "type" );
            AssertArgumentNull( methodName, "methodName" );
            AssertArgumentNull( argTypes, "argTypes" );
            AssertArgumentNull( args, "args" );

            Log.Debug( String.Format( "Invoke method {1} from {0}", type.FullName, methodName ) );

            MethodInfo method = GetMethod( type, methodName, argTypes, BindingFlags_PublicStatic );
            return method.Invoke( null, args );
        }

        public static object InvokeMethod( object instance, MethodInfo method, params object[] args )
        {
            return method.Invoke( instance, args );
        }

        #endregion invoke method

        #region get/set property

        public static object GetProperty( object instance, string propertyName )
        {
            AssertArgumentNull( instance, "instance" );
            PropertyInfo property = instance.GetType().GetProperty( propertyName );
            return property.GetValue( instance, null );
        }

        public static object GetProperty( Type type, object instance, string propertyName )
        {
            AssertArgumentNull( instance, "instance" );
            PropertyInfo property = type.GetProperty( propertyName );
            return property.GetValue( instance, null );
        }

        public static void SetProperty( object instance, string propertyName, object value )
        {
            AssertArgumentNull( instance, "instance" );
            PropertyInfo property = instance.GetType().GetProperty( propertyName, BindingFlags_Instance );
            property.SetValue( instance, value, null );
        }

        public static void SetProperty( Type type, object instance, string propertyName, object value )
        {
            AssertArgumentNull( instance, "instance" );
            PropertyInfo property = type.GetProperty( propertyName, BindingFlags_Instance );
            property.SetValue( instance, value, null );
        }

        public static object GetStaticProperty( Type type, string propertyName )
        {
            AssertArgumentNull( type, "type" );
            PropertyInfo property = type.GetProperty( propertyName, BindingFlags_Static );
            return property.GetValue( null, null );
        }

        public static void SetStaticProperty( Type type, string propertyName, object value )
        {
            AssertArgumentNull( type, "type" );
            PropertyInfo property = type.GetProperty( propertyName, BindingFlags_Static );
            property.SetValue( null, value, null );
        }

        #endregion get/set property

        public static readonly BindingFlags BindingFlags_All = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        public static readonly BindingFlags BindingFlags_Public = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
        public static readonly BindingFlags BindingFlags_Static = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        public static readonly BindingFlags BindingFlags_Instance = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public static readonly BindingFlags BindingFlags_PublicStatic = BindingFlags.Public | BindingFlags.Static;
        public static readonly BindingFlags BindingFlags_PublicInstance = BindingFlags.Public | BindingFlags.Instance;

        #region assert

        private static void AssertArgumentNull( object arg, string argName )
        {
            if ( arg == null )
            {
                throw new ArgumentNullException( argName );
            }
        }

        private static void AssertIsSubClassOf( Type type, Type baseType )
        {
            if ( !type.IsAssignableFrom( baseType ) )
            {
                throw new ArgumentException( "typeName 指定的类不是 baseType 的继承类" );
            }
        }

        #endregion assert
    }
}