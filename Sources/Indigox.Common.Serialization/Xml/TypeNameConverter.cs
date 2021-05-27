using System;

namespace Indigox.Common.Serialization.Xml
{
    class TypeNameConverter
    {
        public virtual string GetTypeName( Type type )
        {
            if ( type.IsGenericType )
            {
                Type[] genericArgs = type.GetGenericArguments();
                string[] genericArgNames = new string[ genericArgs.Length ];
                for ( int i = 0 ; i < genericArgs.Length ; i++ )
                {
                    genericArgNames[ i ] = "[" + GetTypeName( genericArgs[ i ] ) + "]";
                }
                return string.Format( "{0}.{1}[{3}], {2}", type.Namespace, type.Name, type.Assembly.GetName().Name, string.Join( ",", genericArgNames ) );
            }
            else
            {
                return string.Format( "{0}, {1}", type.FullName, type.Assembly.GetName().Name );
            }
        }
    }
}
