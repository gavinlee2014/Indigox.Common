using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Indigox.Common.Expression.Interface
{
    public interface IExpressionContext
    {
        void SetProperty( string name, object value );
        void SetFunction( string prefix, string name, Type type, string methodName );
        void SetFunctions( string prefix, Type type );
        void SetFunction( string prefix, string name, object instance, string methodName );
        void SetFunctions( string prefix, object instance );
        object GetProperty( string name );
        MethodInfo GetFunction( string name );
        object GetFunctionInstance( string functionName );
    }
}
