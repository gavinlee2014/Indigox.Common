using System;
using Indigox.Common.DomainModels.Interface.Specifications;
using NHibernate;
using NHibernate.Criterion;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts
{
    internal interface ISpecificationConvertor
    {
        ICriterion Convert<T>( ICriteria criteria, ISpecification specification, int specificationLevel );
    }
}