using System;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class Specification
    {
        public static ISpecification All()
        {
            return new AllSpecification();
        }

        #region logic specifications

        public static ISpecification And( params ISpecification[] specifications )
        {
            return new AndSpecification( specifications );
        }

        public static ISpecification Or( params ISpecification[] specifications )
        {
            return new OrSpecification( specifications );
        }

        public static ISpecification Not( ISpecification specification )
        {
            return new NotSpecification( specification );
        }

        #endregion logic specifications

        #region relational specifications

        public static ISpecification Equal( string propertyName, object value )
        {
            return new EqualSpecification( propertyName, value );
        }

        public static ISpecification NotEqual( string propertyName, object value )
        {
            return new NotEqualSpecification( propertyName, value );
        }

        public static ISpecification GreaterThan( string propertyName, object value )
        {
            return new GreaterThanSpecification( propertyName, value );
        }

        public static ISpecification LessThan( string propertyName, object value )
        {
            return new LessThanSpecification( propertyName, value );
        }

        public static ISpecification GreaterOrEqual( string propertyName, object value )
        {
            return new GreaterOrEqualSpecification( propertyName, value );
        }

        public static ISpecification LessOrEqual( string propertyName, object value )
        {
            return new LessOrEqualSpecification( propertyName, value );
        }

        public static ISpecification Contains( string propertyName, object value )
        {
            return new ContainsSpecification( propertyName, value );
        }

        public static ISpecification ContainsAny( string propertyName, object value )
        {
            return new ContainsAnySpecification( propertyName, value );
        }

        public static ISpecification In( string propertyName, object[] value )
        {
            return new InSpecification( propertyName, value );
        }

        public static ISpecification Like( string propertyName, object value )
        {
            return new LikeSpecification( propertyName, value );
        }

        #endregion relational specifications
    }
}