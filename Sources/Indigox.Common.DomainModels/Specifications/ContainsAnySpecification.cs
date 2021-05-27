using System;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class ContainsAnySpecification : RelationalSpecification, ISpecification
    {
        public ContainsAnySpecification( string propertyName, object value )
            : base( propertyName, value )
        {
        }

        public override SpecificationType Type
        {
            get { return SpecificationType.ContainsAny; }
        }

        public override bool IsStatisfiedBy( object entity )
        {
            throw new NotImplementedException();
        }
    }
}
