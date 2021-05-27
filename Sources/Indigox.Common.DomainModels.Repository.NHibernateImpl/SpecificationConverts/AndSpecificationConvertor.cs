using System;
using Indigox.Common.DomainModels.Interface.Specifications;
using NHibernate;
using NHibernate.Criterion;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts
{
    internal class AndSpecificationConvertor : ISpecificationConvertor
    {
        public ICriterion Convert<T>( ICriteria criteria, ISpecification specification, int specificationLevel )
        {
            Conjunction conjunction = Restrictions.Conjunction();
            foreach ( ISpecification spec in specification.ComparingValue )
            {
                conjunction.Add(
                    SpecificationConvertors.GetConvertor( spec )
                        .Convert<T>( criteria, spec, specificationLevel + 1 ) );
            }
            return conjunction;
        }
    }
}