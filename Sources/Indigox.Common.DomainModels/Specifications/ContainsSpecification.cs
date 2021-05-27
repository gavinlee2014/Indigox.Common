using System;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class ContainsSpecification : RelationalSpecification, ISpecification
    {
        public ContainsSpecification(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public override SpecificationType Type
        {
            get { return SpecificationType.Contains; }
        }

        public override bool IsStatisfiedBy(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
