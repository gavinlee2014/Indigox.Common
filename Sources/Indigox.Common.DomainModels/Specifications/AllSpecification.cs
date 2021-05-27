using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class AllSpecification : ISpecification
    {
        public string PropertyName
        {
            get { return null; }
        }

        public System.Collections.IList ComparingValue
        {
            get { return null; }
        }

        public SpecificationType Type
        {
            get { return SpecificationType.All; }
        }

        public bool IsStatisfiedBy(object entity)
        {
            return true;
        }
    }
}
