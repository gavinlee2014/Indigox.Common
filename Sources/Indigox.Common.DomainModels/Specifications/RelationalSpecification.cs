using System;
using System.Collections;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    /// <summary>
    /// 关系运算符，它的实现类包括
    /// <see cref="EqualSpecification"/>、
    /// <see cref="GreaterThanSpecification"/>、
    /// <see cref="LessThanSpecification"/>、
    /// <see cref="GreaterOrEqualSpecification"/>、
    /// <see cref="LessOrEqualSpecification"/>、
    /// <see cref="NotEqualSpecification"/>、
    /// <see cref="ContainsSpecification"/>、
    /// <see cref="ContainsInSpecification"/>
    /// </summary>
    public abstract class RelationalSpecification : ISpecification
    {
        private string propertyName;
        private object[] comparingValue;

        protected RelationalSpecification(string propertyName, object value)
        {
            this.propertyName = propertyName;
            this.comparingValue = new object[] { value };
        }

        public string PropertyName
        {
            get { return this.propertyName; }
        }

        IList ISpecification.ComparingValue
        {
            get { return Array.AsReadOnly(comparingValue); }
        }

        public object Value
        {
            get { return comparingValue[0]; }
        }

        public abstract SpecificationType Type
        {
            get;
        }

        public abstract bool IsStatisfiedBy(object entity);
    }
}
