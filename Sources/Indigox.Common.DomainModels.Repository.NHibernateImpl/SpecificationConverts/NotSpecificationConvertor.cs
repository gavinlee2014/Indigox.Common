using System;
using Indigox.Common.DomainModels.Interface.Specifications;
using NHibernate;
using NHibernate.Criterion;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts
{
    internal class NotSpecificationConvertor : ISpecificationConvertor
    {
        public ICriterion Convert<T>( ICriteria criteria, ISpecification specification, int specificationLevel )
        {
            ISpecification spec = (ISpecification)specification.ComparingValue[ 0 ];
            return Restrictions.Not(
                SpecificationConvertors.GetConvertor( spec )
                    .Convert<T>( criteria, spec, specificationLevel ) );
        }
    }
}