using System;
using System.Text.RegularExpressions;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Specifications
{
    public class LikeSpecification : PropertySpecification, ISpecification
    {
        public LikeSpecification( string propertyName, object value )
            : base( propertyName, value )
        {
        }

        public override SpecificationType Type
        {
            get { return SpecificationType.Like; }
        }

        public override bool IsStatisfiedBy( object entity )
        {
            bool satisfied = false;
            Type t = entity.GetType();
            object val = null;
            if ( this.TryGetPropertyValue( entity, ref val ) )
            {
                if ( StringLike( (string)val, (string)this.Value ) )
                {
                    satisfied = true;
                }
            }
            return satisfied;
        }

        private bool StringLike( string input, string pattern )
        {
            pattern = "^" + pattern.Replace( "%", ".*" ) + "$";
            Regex regex = new Regex( pattern );
            return regex.IsMatch( input );
        }
    }
}