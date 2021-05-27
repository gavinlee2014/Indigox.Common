using System.Reflection;
using Indigox.Common.Expression.Interface;

namespace Indigox.Common.Expression
{
    public class ExpressionEvaluator : ExpressionEvalBase
    {
        #region Private Instance Fields

        IExpressionContext context;

        #endregion Private Instance Fields

        #region Public Instance Constructors

        public ExpressionEvaluator( IExpressionContext context )
        {
            this.context = context;
        }

        #endregion Public Instance Constructors

        #region Public Instance Properties

        public IExpressionContext Context
        {
            get
            {
                return this.context;
            }
        }

        #endregion Public Instance Properties

        #region Override implementation of ExpressionEvalBase

        protected override object EvaluateProperty( string propertyName )
        {
            return this.Context.GetProperty( propertyName );
        }

        protected override ParameterInfo[] GetFunctionParameters( string functionName )
        {
            MethodInfo methodInfo = this.Context.GetFunction( functionName );
            return methodInfo.GetParameters();
        }

        protected override object EvaluateFunction( string functionName, object[] args )
        {
            MethodInfo methodInfo = this.Context.GetFunction( functionName );

            try
            {
                if ( methodInfo.IsStatic )
                {
                    return methodInfo.Invoke( null, args );
                }
                else if ( methodInfo.DeclaringType.IsAssignableFrom( this.GetType() ) )
                {
                    return methodInfo.Invoke( this, args );
                }
                else
                {
                    object o = this.Context.GetFunctionInstance( functionName );
                    return methodInfo.Invoke( o, args );
                }
            }
            catch ( TargetInvocationException ex )
            {
                if ( ex.InnerException != null )
                {
                    // throw actual exception
                    throw ex.InnerException;
                }
                // re-throw exception
                throw;
            }
        }

        #endregion Override implementation of ExpressionEvalBase
    }
}
