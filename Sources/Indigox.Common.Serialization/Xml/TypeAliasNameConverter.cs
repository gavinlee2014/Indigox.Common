using System;

namespace Indigox.Common.Serialization.Xml
{
    class TypeAliasNameConverter : TypeNameConverter
    {
        private TypeAlias typeAlias;

        public TypeAliasNameConverter( TypeAlias typeAlias )
        {
            this.typeAlias = typeAlias;
        }

        public override string GetTypeName( Type type )
        {
            string alias = typeAlias.GetAlias( type );
            if ( alias != null )
            {
                return alias;
            }
            else
            {
                return base.GetTypeName( type );
            }
        }
    }
}
