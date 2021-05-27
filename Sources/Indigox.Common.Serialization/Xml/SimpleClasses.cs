using System;
using System.Collections.Generic;

namespace Indigox.Common.Serialization.Xml
{
    internal class SimpleClasses
    {
        private IDictionary<Type, IObjectWriter> registedTypes = new Dictionary<Type, IObjectWriter>();

        public void RegistType( Type type, IObjectWriter writer )
        {
            registedTypes.Add( type, writer );
        }

        public IObjectWriter GetWriter( Type type )
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