using System;
using System.Collections;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class InSpecification : PropertySpecification, ISpecification
    {
        public InSpecification( string propertyName, object[] value )
            : base( propertyName, value )
        {
        }

        public override SpecificationType Type
        {
            get { return SpecificationType.In; }
        }

        public override bool IsStatisfiedBy( object entity )
        {
            bool satisfied = false;
            Type t = entity.GetType();
            object val = null;

            if ( !( this.Value is IEnumerable ) )
            {
                throw new Exception( "need enumerable value." );
            }

            IEnumerable em = (IEnumerable)this.Value;

            if ( this.TryGetPropertyValue( entity, ref val ) )
            {
                foreach ( object item in em )
                {
                    if ( ( (IComparable)val ).CompareTo( ( (IComparable)item ) ) >= 0 )
                    {
                        satisfied = true;
                        break;
                    }
                }
            }
            return satisfied;
        }
    }
}
