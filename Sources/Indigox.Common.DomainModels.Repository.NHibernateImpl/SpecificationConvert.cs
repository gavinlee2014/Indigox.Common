using System;
using Indigox.Common.DomainModels.Interface.Specifications;
using Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts;
using NHibernate;
using NHibernate.Criterion;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl
{
    internal class SpecificationConvert
    {
        public static void GetNHibernateCriterion<T>( ICriteria criteria, ISpecification specification )
        {
            ISpecificationConvertor convertor = SpecificationConvertors.GetConvertor( specification );
            ICriterion criterion = convertor.Convert<T>( criteria, specification, 1 );
            criteria.Add( criterion );
        }
    }
}