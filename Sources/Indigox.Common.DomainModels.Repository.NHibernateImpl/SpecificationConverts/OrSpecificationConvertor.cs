using System;
using Indigox.Common.DomainModels.Interface.Specifications;
using NHibernate;
using NHibernate.Criterion;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts
{
    internal class OrSpecificationConvertor : ISpecificationConvertor
    {
        public ICriterion Convert<T>( ICriteria criteria, ISpecification specification, int specificationLevel )
        {
            Disjunction disjunction = Restrictions.Disjunction();
            foreach ( ISpecification spec in specification.ComparingValue )
            {
                disjunction.Add(
                    SpecificationConvertors.GetConvertor( spec )
                        .Convert<T>( criteria, spec, specificationLevel + 1 ) );
            }
            return disjunction;
        }
    }
}