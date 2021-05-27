using System;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.SpecificationConverts
{
    internal class SpecificationConvertors
    {
        public static ISpecificationConvertor GetConvertor( ISpecification specification )
        {
            if ( specification.Type == SpecificationType.Equal )
            {
                return new EqualSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.NotEqual )
            {
                return new NotEqualSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.GreaterThan )
            {
                return new GreaterThanSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.GraterOrEqual )
            {
                return new GreaterOrEqualSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.LessThan )
            {
                return new LessThanSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.LessOrEqual )
            {
                return new LessOrEqualSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.Contains )
            {
                return new ContainsSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.ContainsAny )
            {
                return new ContainsAnySpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.In )
            {
                return new InSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.Like )
            {
                return new LikeSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.And )
            {
                return new AndSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.Or )
            {
                return new OrSpecificationConvertor();
            }
            else if ( specification.Type == SpecificationType.Not )
            {
                return new NotSpecificationConvertor();
            }
            else
            {
                throw new Exception( "Can't find specification convertor for '" + specification.Type + "'." );
            }
        }
    }
}