using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Test.Repository.NHibernateImpl
{
    class SpecificationConvert
    {
        public static ICriterion GetNHibernateCriterion(ISpecification specification)
        {
            if (specification.Type == SpecificationType.Equal)
            {
                return Restrictions.Eq(specification.PropertyName, specification.ComparingValue[0]);
            }
            else if (specification.Type == SpecificationType.NotEqual)
            {
                return Restrictions.Not(Restrictions.Eq(specification.PropertyName, specification.ComparingValue[0]));
            }
            else if (specification.Type == SpecificationType.GreaterThan)
            {
                return Restrictions.Gt(specification.PropertyName, specification.ComparingValue[0]);
            }
            else if (specification.Type == SpecificationType.LessThan)
            {
                return Restrictions.Lt(specification.PropertyName, specification.ComparingValue[0]);
            }
            else if (specification.Type == SpecificationType.And)
            {
                Conjunction conj = Restrictions.Conjunction();
                foreach (ISpecification spec in specification.ComparingValue)
                {
                    conj.Add(GetNHibernateCriterion(spec));
                }
                return conj;
            }
            else if (specification.Type == SpecificationType.Or)
            {
                Disjunction disj = Restrictions.Disjunction();
                foreach (ISpecification spec in specification.ComparingValue)
                {
                    disj.Add(GetNHibernateCriterion(spec));
                }
                return disj;
            }
            return null;
        }
    }
}
