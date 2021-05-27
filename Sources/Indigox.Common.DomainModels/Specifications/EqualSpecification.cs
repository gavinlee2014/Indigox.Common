using System;
using System.Reflection;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class EqualSpecification : PropertySpecification, ISpecification
    {
        public EqualSpecification( string propertyName, object value )
            : base( propertyName, value )
        {
        }

        public override SpecificationType Type
        {
            get { return SpecificationType.Equal; }
        }

        public override bool IsStatisfiedBy( object entity )
        {
            bool satisfied = false;
            Type t = entity.GetType();
            object val = null;
            if ( this.TryGetPropertyValue( entity, ref val ) )
            {
                if ( ( this.Value == val ) || ( this.Value.Equals( val ) ) )
                {
                    satisfied = true;
                }
            }
            return satisfied;
        }
    }
}
