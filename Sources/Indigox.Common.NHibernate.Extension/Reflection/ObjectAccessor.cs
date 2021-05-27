using System;
using System.Reflection;

namespace Indigox.Common.NHibernate.Extension.Reflection
{
    public class ObjectAccessor
    {
        public static object GetProperty( object instance, string propertyName )
        {
            return GetProperty( instance, propertyName, instance.GetType() );
        }

        public static object GetProperty( object instance, string propertyName, Type constraintType )
        {
            PropertyInfo property = constraintType.GetProperty( propertyName );
            return property.GetValue( instance, null );
        }

        public static void SetProperty( object instance, string propertyName, object value )
        {
            SetProperty( instance, propertyName, value, instance.GetType() );
        }

        public static void SetProperty( object instance, string propertyName, object value, Type constraintType )
        {
            PropertyInfo property = constraintType.GetProperty( propertyName );
            property.SetValue( instance, value, null );
        }
    }
}