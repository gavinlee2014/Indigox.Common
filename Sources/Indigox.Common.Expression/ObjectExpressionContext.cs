using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Indigox.Common.Expression.Interface;
using Indigox.Common.Utilities;
using Indigox.Common.Logging;

namespace Indigox.Common.Expression
{
    public class ObjectExpressionContext : IExpressionContext
    {
        public ObjectExpressionContext( Object contextObject )
        {
            this.contextObject = contextObject;
        }

        private Object contextObject;
        private ContextFunctions functions = new ContextFunctions();

        public void SetProperty( string name, object value )
        {
            throw new NotSupportedException();
        }

        public void SetFunction( string prefix, string name, Type type, string methodName )
        {
            functions.SetFunction( prefix, name, type, methodName );
        }

        public void SetFunctions( string prefix, Type type )
        {
            functions.SetFunctions( prefix, type );
        }

        public void SetFunction( string prefix, string name, object instance, string methodName )
        {
            functions.SetFunction( prefix, name, instance, methodName );
        }

        public void SetFunctions( string prefix, object instance )
        {
            functions.SetFunctions( prefix, instance );
        }

        public object GetProperty( string name )
        {
            //Log.Debug( "property name: " + name );
            try
            {
                return GetProperty( this.contextObject, name, "" );
            }
            catch ( Exception ex )
            {
                throw new Exception( string.Format( "Can't get property [{0}].", name ), ex );
            }
        }

        private object GetProperty( object evaluatedPropertyValue, string propertyName, string evaluatedProperty )
        {
            //Log.Debug( string.Format( "Get property from context, name = '{0}'", name ) );

            string name1 = propertyName;
            string name2 = null;
            string name3 = evaluatedProperty;
            bool hasSubProperty = propertyName.Contains( "." );
            if ( hasSubProperty )
            {
                name1 = propertyName.Substring( 0, propertyName.IndexOf( "." ) );
                name2 = propertyName.Substring( propertyName.IndexOf( "." ) + 1 );
            }

            object val = null;
            if ( evaluatedPropertyValue is IDictionary<string, object> )
            {
                IDictionary<string, object> dict = (IDictionary<string, object>)evaluatedPropertyValue;
                if ( dict.ContainsKey( name1 ) )
                {
                    val = dict[ name1 ];
                }
                else
                {
                    throw new NullReferenceException( string.Format( "Dictionary [{1}] not contains [{0}].", name1, name3 ) );
                }
            }
            else if ( evaluatedPropertyValue is IDictionary )
            {
                IDictionary dict = (IDictionary)evaluatedPropertyValue;
                if ( dict.Contains( name1 ) )
                {
                    val = dict[ name1 ];
                }
                else
                {
                    throw new NullReferenceException( string.Format( "Dictionary [{1}] not contains [{0}].", name1, name3 ) );
                }
            }
            else
            {
                val = ReflectUtil.GetProperty( evaluatedPropertyValue, name1 );
            }

            name3 += ( ( name3.Length > 0 ) ? "." : "" ) + name1;

            if ( hasSubProperty )
            {
                if ( val == null )
                {
                    throw new NullReferenceException( string.Format( "Property [{0}] is null.", name3 ) );
                }
                return GetProperty( val, name2, name3 );
            }
            else
            {
                return val;
            }
        }

        public MethodInfo GetFunction( string name )
        {
            return functions.GetFunction( name );
        }

        public object GetFunctionInstance( string name )
        {
            return functions.GetInstance( name );
        }
    }
}
