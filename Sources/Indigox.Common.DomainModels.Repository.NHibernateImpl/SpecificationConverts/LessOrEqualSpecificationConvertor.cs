using System;
using Indigox.Common.DomainModels.Interface.Specifications;
using NHibernate;
using NHibernate.Criterion;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts
{
    internal class LessOrEqualSpecificationConvertor : ISpecificationConvertor
    {
        public ICriterion Convert<T>( ICriteria criteria, ISpecification specification, int specificationLevel )
        {
            string prop = specification.PropertyName;
            object val = specification.ComparingValue[ 0 ];
            return Restrictions.Le( prop, val );
        }
    }
}