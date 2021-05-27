using System;
using System.Collections;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Interface.Specifications;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts
{
    internal class ContainsAnySpecificationConvertor : ISpecificationConvertor
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
            if ( !( val is IEnumerable ) )
            {
                throw new Exception( "ContainsAnySpecification 的值必须是一个数组 ( Property = " + prop + " )" );
            }

            IEnumerable vals = (IEnumerable)val;
            if ( !vals.GetEnumerator().MoveNext() )
            {
                throw new Exception( "ContainsAnySpecification 的值数组至少要包含一个值 ( Property = " + prop + " )" );
            }

            IType elementType = ClassMetadataHelper.GetCollectionElementType( entityType, prop );
            List<ICriterion> criterions = new List<ICriterion>();
            if ( !elementType.IsEntityType )
            {
                string fkElementColumn = ClassMetadataHelper.GetCollectionElementColumn( entityType, prop );

                foreach ( var subval in (IEnumerable)val )
                {
                    criterions.Add( Expression.Sql( fkElementColumn + "=?", subval, elementType ) );
                }
            }
            else
            {
                foreach ( var subval in (IEnumerable)val )
                {
                    object elementIdentityValue = ClassMetadataHelper.GetIdentityValue( elementType.ReturnedClass, subval );
                    criterions.Add( Expression.IdEq( elementIdentityValue ) );
                }
            }

            ICriterion subqueryCriterion = criterions[ 0 ];
            for ( int i = 1; i < criterions.Count; i++ )
            {
                subqueryCriterion = Expression.Or( subqueryCriterion, criterions[ i ] );
            }
            return subqueryCriterion;
        }
    }
}