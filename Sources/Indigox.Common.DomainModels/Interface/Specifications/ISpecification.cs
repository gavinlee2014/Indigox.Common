using System.Collections;

namespace Indigox.Common.DomainModels.Interface.Specifications
{
    public interface ISpecification
    {
        string PropertyName { get; }
        IList ComparingValue { get; }
        SpecificationType Type { get; }
        bool IsStatisfiedBy(object entity);
    }
}
