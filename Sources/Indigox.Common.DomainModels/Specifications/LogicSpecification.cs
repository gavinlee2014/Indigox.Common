using System;
using System.Collections;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public abstract class LogicSpecification : ISpecification
    {
        private ISpecification[] specifications;

        protected LogicSpecification(ISpecification[] specifications)
        {
            this.specifications = specifications;
        }

        string ISpecification.PropertyName
        {
            get { return null; }
        }

        IList ISpecification.ComparingValue
        {
            get { return Array.AsReadOnly<ISpecification>(this.specifications); }
        }

        public IList<ISpecification> Specifications
        {
            get { return Array.AsReadOnly<ISpecification>(this.specifications); }
        }

        public abstract SpecificationType Type
        {
            get;
        }

        public abstract bool IsStatisfiedBy(object entity);
    }
}
