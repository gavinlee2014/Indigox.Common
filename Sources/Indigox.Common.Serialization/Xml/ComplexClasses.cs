using System;
using System.Collections.Generic;

namespace Indigox.Common.Serialization.Xml
{
    internal class ComplexClasses
    {
        private IDictionary<Type, IObjectWriter> registedTypes = new Dictionary<Type, IObjectWriter>();

        public void RegistType( Type type, IObjectWriter writer )
        {
            registedTypes.Add( type, writer );
        }

        public IObjectWriter GetWriter( Type type )
        {
            Type _type = null;
            foreach ( Type registedType in registedTypes.Keys )
            {
                if ( registedType.IsAssignableFrom( type ) )
                {
                    _type = registedType;
                }
                else if ( type.IsGenericType && registedType.IsGenericTypeDefinition )
                {
                    if ( type.GetGenericArguments().Length != registedType.GetGenericArguments().Length )
                    {
                        continue;
                    }

                    Type genericRegistedType = registedType.MakeGenericType( type.GetGenericArguments() );

                    if ( genericRegistedType.IsAssignableFrom( type ) )
                    {
                        _type = registedType;
                    }

                }

                if ( _type != null )
                {
                    return registedTypes[ _type ];
                }
            }

            return null;
        }
    }
}