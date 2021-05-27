using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class AndSpecification : LogicSpecification, ISpecification
    {
        public AndSpecification(params ISpecification[] specifications)
            : base(specifications)
        {
        }

        public override SpecificationType Type
        {
            get { return SpecificationType.And; }
        }

        public override bool IsStatisfiedBy(object entity)
        {
            bool satisfied = true;
            foreach (ISpecification spec in this.Specifications)
            {
                if (!spec.IsStatisfiedBy(entity))
                {
                    satisfied = false;
                    break;
                }
            }
            return satisfied;
        }
    }
}
