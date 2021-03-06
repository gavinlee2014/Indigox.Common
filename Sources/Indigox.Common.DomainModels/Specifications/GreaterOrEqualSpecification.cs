using System;
using System.Reflection;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class GreaterOrEqualSpecification : PropertySpecification, ISpecification
    {
        public GreaterOrEqualSpecification( string propertyName, object value )
            : base( propertyName, value )
        {
        }

        public override SpecificationType Type
        {
            get { return SpecificationType.GraterOrEqual; }
        }

        public override bool IsStatisfiedBy( object entity )
        {
            bool satisfied = false;
            Type t = entity.GetType();
            object val = null;
            if ( this.TryGetPropertyValue( entity, ref val ) )
            {
                if ( ( (IComparable)val ).CompareTo( ( (IComparable)this.Value ) ) >= 0 )
                {
                    satisfied = true;
                }
            }
            return satisfied;
        }
    }
}
