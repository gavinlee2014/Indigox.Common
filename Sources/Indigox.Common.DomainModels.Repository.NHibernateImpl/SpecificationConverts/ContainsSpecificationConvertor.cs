using System;
using Indigox.Common.DomainModels.Interface.Specifications;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts
{
    internal class ContainsSpecificationConvertor : ISpecificationConvertor
    {
        public ICriterion Convert<T>( ICriteria criteria, ISpecification specification, int specificationLevel )
        {
            string prop = specification.PropertyName;
            object val = specification.ComparingValue[ 0 ];
            Type entityType = InstanceTypeMapping.GetMappedClass<T>();
            string entityKeyProperty = ClassMetadataHelper.GetIdentityProperty( entityType );

            ICriterion subqueryCriterion = GetSubQueryCriterion( entityType, prop, val );

            ICriterion subquery = Subqueries.PropertyIn( entityKeyProperty, // key
                DetachedCriteria.For( entityType )
                                .CreateCriteria( prop )  // property
                                .Add( subqueryCriterion )
                                .SetProjection( Projections.Property( entityKeyProperty ) ) ); // key

            return subquery;
        }

        private ICriterion GetSubQueryCriterion( Type entityType, string prop, object val )
        {
            IType elementType = ClassMetadataHelper.GetCollectionElementType( entityType, prop );
            ICriterion subqueryCriterion = null;
            if ( !elementType.IsEntityType )
            {
                string fkElementColumn = ClassMetadataHelper.GetCollectionElementColumn( entityType, prop );
                subqueryCriterion = Expression.Sql( fkElementColumn + "=?", val, elementType );
            }
            else
            {
                object elementIdentityValue = ClassMetadataHelper.GetIdentityValue( elementType.ReturnedClass, val );
                subqueryCriterion = Expression.IdEq( elementIdentityValue );
            }
            return subqueryCriterion;
        }
    }
}