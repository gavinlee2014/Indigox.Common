using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class OrSpecification : LogicSpecification, ISpecification
    {
        public OrSpecification(params ISpecification[] specifications)
            : base(specifications)
        {
        }

        public override SpecificationType Type
        {
            get { return SpecificationType.Or; }
        }

        public override bool IsStatisfiedBy(object entity)
        {
            bool satisfied = false;
            foreach (ISpecification spec in this.Specifications)
            {
                if (spec.IsStatisfiedBy(entity))
                {
                    satisfied = true;
                    break;
                }
            }
            return satisfied;
        }
    }
}
