using System;
using Indigox.Common.DomainModels.Interface.Specifications;
using NHibernate;
using NHibernate.Criterion;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts
{
    internal class NotEqualSpecificationConvertor : ISpecificationConvertor
    {
        public ICriterion Convert<T>( ICriteria criteria, ISpecification specification, int specificationLevel )
        {
            string prop = specification.PropertyName;
            object val = specification.ComparingValue[ 0 ];
            if ( val == null )
            {
                return Restrictions.IsNotNull( prop );
            }
            else
            {
                return Restrictions.Not( Restrictions.Eq( prop, val ) );
            }
        }
    }
}