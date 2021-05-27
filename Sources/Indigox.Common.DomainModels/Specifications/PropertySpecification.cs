using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.DomainModels.Interface.Specifications;
using System.Reflection;

namespace Indigox.Common.DomainModels.Specifications
{
    public abstract class PropertySpecification : RelationalSpecification, ISpecification
    {
        private static readonly BindingFlags ReflectPropertyBindingFlags =
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.FlattenHierarchy;


        protected PropertySpecification( string propertyName, object value )
            : base( propertyName, value )
        {
        }

        protected bool TryGetPropertyValue( object entity, ref object propertyValue )
        {
            Type t = entity.GetType();
            PropertyInfo[] properties = t.GetProperties( ReflectPropertyBindingFlags );
            foreach ( PropertyInfo prop in properties )
            {
                if ( prop.Name.Equals( this.PropertyName ) )
                {
                    propertyValue = prop.GetValue( entity, null );
                    return true;
                }
            }
            return false;
        }
    }
}
