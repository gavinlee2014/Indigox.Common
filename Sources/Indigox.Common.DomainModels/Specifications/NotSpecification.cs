using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class NotSpecification : LogicSpecification, ISpecification
    {
        public NotSpecification(ISpecification specifications)
            : base(new ISpecification[] { specifications })
        {
        }

        public override SpecificationType Type
        {
            get { return SpecificationType.Not; }
        }

        public override bool IsStatisfiedBy(object entity)
        {
            return !(this.Specifications[0].IsStatisfiedBy(entity));
        }
    }
}
