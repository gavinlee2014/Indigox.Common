using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Expression.Interface
{
    interface IExpressionEvaluator
    {
        IExpressionContext Context { get; }
        void CheckSyntax( string s );
        object Evaluate( string s );
        object Evaluate( IExpressionTokenizer token );
        object GetPropertyValue(string name);
    }
}
