using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Serialization.Xml
{
    class TypeAlias
    {
        private IDictionary<Type, string> registedTypes = new Dictionary<Type, string>();

        public void RegistType( Type type, string alias )
        {
            registedTypes.Add( type, alias );
        }

        public string GetAlias( Type type )
        {
            if ( registedTypes.ContainsKey( type ) )
            {
                return registedTypes[ type ];
            }
            else
            {
                return null;
            }
        }
    }
}
