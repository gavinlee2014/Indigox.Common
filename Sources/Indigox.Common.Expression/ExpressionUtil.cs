using System;
using System.Text.RegularExpressions;
using Indigox.Common.Expression.Interface;
using Indigox.Common.Logging;

namespace Indigox.Common.Expression
{
    public class ExpressionUtil
    {
        private static readonly Regex PropertyTokenRegex = new Regex( @"\${([^{}]+)}" );
        private static readonly Regex OnlyOnePropertyTokenRegex = new Regex( @"^\${([^{}]+)}$" );

        private IExpressionContext expressionContext;

        public ExpressionUtil( IExpressionContext expressionContext )
        {
            this.expressionContext = expressionContext;
        }

        public string Replace( string plainText )
        {
            string replacedText = PropertyTokenRegex.Replace( plainText, new MatchEvaluator( MatchReplace ) );
            return replacedText;
        }

        public bool TryReplace( string plainText, ref string replacedText )
        {
            try
            {
                string temp = Replace( plainText );
                replacedText = temp;
                return true;
            }
            catch ( Exception ex )
            {
                Log.Warn( "无法计算表达式：\"" + plainText + "\" ----> " + ex.Message );
                return false;
            }
        }

        public object Evaluate( string plainText )
        {
            Match match = OnlyOnePropertyTokenRegex.Match( plainText );
            if ( match.Success )
            {
                string expression = match.Groups[ 1 ].Value;
                ExpressionEvaluator evaluator = new ExpressionEvaluator( expressionContext );
                return evaluator.Evaluate( expression );
            }
            else
            {
                return Replace( plainText );
            }
        }

        public bool TryEvaluate( string plainText, ref object evaluated )
        {
            try
            {
                object temp = Evaluate( plainText );
                evaluated = temp;
                return true;
            }
            catch ( Exception ex )
            {
                Log.Warn( "无法计算表达式：\"" + plainText + "\" ----> " + ex.Message );
                return false;
            }
        }

        private string MatchReplace( Match match )
        {
            string expression = match.Groups[ 1 ].Value;
            ExpressionEvaluator evaluator = new ExpressionEvaluator( expressionContext );
            object value = evaluator.Evaluate( expression );
            return ( value == null ) ? string.Empty : value.ToString();
        }
    }
}