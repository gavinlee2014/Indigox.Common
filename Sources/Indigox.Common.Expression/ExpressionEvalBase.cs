using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Indigox.Common.Expression.Interface;
using Indigox.Common.Expression.Utils;

namespace Indigox.Common.Expression
{
    public abstract class ExpressionEvalBase
    {

        enum EvalMode
        {
            Evaluate,
            ParseOnly
        }

        private EvalMode _evalMode = EvalMode.Evaluate;

        private IExpressionTokenizer _tokenizer = null;
        public ExpressionEvalBase() { }

        public object Evaluate( IExpressionTokenizer tokenizer )
        {
            _evalMode = EvalMode.Evaluate;
            _tokenizer = tokenizer;
            return ParseExpression();
        }

        public object Evaluate( string s )
        {
            _tokenizer = new ExpressionTokenizer();
            _evalMode = EvalMode.Evaluate;

            _tokenizer.InitTokenizer( s );
            object val = ParseExpression();
            if ( _tokenizer.CurrentToken != TokenType.EOF )
            {
                throw BuildParseError( "Unexpected token at the end of expression", _tokenizer.CurrentPosition );
            }
            return val;
        }

        public void CheckSyntax( string s )
        {
            _tokenizer = new ExpressionTokenizer();
            _evalMode = EvalMode.ParseOnly;

            _tokenizer.InitTokenizer( s );
            ParseExpression();
            if ( _tokenizer.CurrentToken != TokenType.EOF )
            {
                throw BuildParseError( "Unexpected token at the end of expression", _tokenizer.CurrentPosition );
            }
        }

        #region Parser

        bool SyntaxCheckOnly()
        {
            return _evalMode == EvalMode.ParseOnly;
        }

        private object ParseExpression()
        {
            return ParseBooleanOr();
        }

        private object ParseBooleanOr()
        {
            Position p0 = _tokenizer.CurrentPosition;
            object o = ParseBooleanAnd();
            EvalMode oldEvalMode = _evalMode;
            try
            {
                while ( _tokenizer.IsKeyword( "or" ) )
                {
                    bool v1 = true;

                    if ( !SyntaxCheckOnly() )
                    {
                        v1 = (bool)SafeConvert( typeof( bool ), o, "the left hand side of the 'or' operator", p0, _tokenizer.CurrentPosition );

                        if ( v1 )
                        {
                            // we're lazy - don't evaluate anything from now, we know that the result is 'true'
                            _evalMode = EvalMode.ParseOnly;
                        }
                    }

                    _tokenizer.GetNextToken();
                    Position p2 = _tokenizer.CurrentPosition;
                    object o2 = ParseBooleanAnd();
                    Position p3 = _tokenizer.CurrentPosition;

                    if ( !SyntaxCheckOnly() )
                    {
                        bool v2 = (bool)SafeConvert( typeof( bool ), o2, "the right hand side of the 'or' operator", p2, p3 );
                        o = v1 || v2;
                    }
                }
                return o;
            }
            finally
            {
                _evalMode = oldEvalMode;
            }
        }

        private object ParseBooleanAnd()
        {
            Position p0 = _tokenizer.CurrentPosition;
            object o = ParseRelationalExpression();
            EvalMode oldEvalMode = _evalMode;
            try
            {
                while ( _tokenizer.IsKeyword( "and" ) )
                {
                    bool v1 = true;

                    if ( !SyntaxCheckOnly() )
                    {
                        v1 = (bool)SafeConvert( typeof( bool ), o, "the left hand side of the 'and' operator", p0, _tokenizer.CurrentPosition );

                        if ( !v1 )
                        {
                            // we're lazy - don't evaluate anything from now, we know that the result is 'true'
                            _evalMode = EvalMode.ParseOnly;
                        }
                    }

                    _tokenizer.GetNextToken();
                    Position p2 = _tokenizer.CurrentPosition;
                    object o2 = ParseRelationalExpression();
                    Position p3 = _tokenizer.CurrentPosition;
                    if ( !SyntaxCheckOnly() )
                    {
                        bool v2 = (bool)SafeConvert( typeof( bool ), o2, "the right hand side of the 'and' operator", p2, p3 );

                        o = v1 && v2;
                    }
                }
                return o;
            }
            finally
            {
                _evalMode = oldEvalMode;
            }
        }

        private object ParseRelationalExpression()
        {
            Position p0 = _tokenizer.CurrentPosition;
            object o = ParseAddSubtract();

            // TODO: remove this after the 0.85 release
            if ( _tokenizer.CurrentToken == TokenType.Punctuation &&
                    _tokenizer.TokenText == "=" )
            {
                throw BuildParseError( "The '=' operator is no longer"
                    + " supported to check for equality. Use the '==' operator"
                    + " instead.", _tokenizer.CurrentPosition );
            }


            if ( _tokenizer.CurrentToken == TokenType.EQ
             || _tokenizer.CurrentToken == TokenType.NE
             || _tokenizer.CurrentToken == TokenType.LT
             || _tokenizer.CurrentToken == TokenType.GT
             || _tokenizer.CurrentToken == TokenType.LE
             || _tokenizer.CurrentToken == TokenType.GE )
            {

                TokenType op = _tokenizer.CurrentToken;
                _tokenizer.GetNextToken();

                object o2 = ParseAddSubtract();
                Position p2 = _tokenizer.CurrentPosition;

                if ( SyntaxCheckOnly() )
                {
                    return null;
                }

                switch ( op )
                {
                    case TokenType.EQ:
                        if ( o is string && o2 is string )
                        {
                            return o.Equals( o2 );
                        }
                        else if ( o is bool && o2 is bool )
                        {
                            return o.Equals( o2 );
                        }
                        else if ( o is int && o2 is int )
                        {
                            return o.Equals( o2 );
                        }
                        else if ( o is int && o2 is long )
                        {
                            return ( Convert.ToInt64( o ) ).Equals( o2 );
                        }
                        else if ( o is int && o2 is double )
                        {
                            return ( Convert.ToDouble( o ) ).Equals( o2 );
                        }
                        else if ( o is long && o2 is long )
                        {
                            return o.Equals( o2 );
                        }
                        else if ( o is long && o2 is int )
                        {
                            return ( o.Equals( Convert.ToInt64( o2 ) ) );
                        }
                        else if ( o is long && o2 is double )
                        {
                            return ( Convert.ToDouble( o ) ).Equals( o2 );
                        }
                        else if ( o is double && o2 is double )
                        {
                            return o.Equals( o2 );
                        }
                        else if ( o is double && o2 is int )
                        {
                            return o.Equals( Convert.ToDouble( o2 ) );
                        }
                        else if ( o is double && o2 is long )
                        {
                            return o.Equals( Convert.ToDouble( o2 ) );
                        }
                        else if ( o is DateTime && o2 is DateTime )
                        {
                            return o.Equals( o2 );
                        }
                        else if ( o is TimeSpan && o2 is TimeSpan )
                        {
                            return o.Equals( o2 );
                        }
                        else if ( o.GetType().IsEnum )
                        {
                            if ( o2 is string )
                            {
                                return o.Equals( Enum.Parse( o.GetType(), (string)o2, false ) );
                            }
                            else
                            {
                                return o.Equals( Enum.ToObject( o.GetType(), o2 ) );
                            }
                        }
                        else if ( o2.GetType().IsEnum )
                        {
                            if ( o is string )
                            {
                                return o2.Equals( Enum.Parse( o2.GetType(), (string)o, false ) );
                            }
                            else
                            {
                                return o2.Equals( Enum.ToObject( o2.GetType(), o ) );
                            }
                        }

                        // Compare with different types
                        else if ( o is string && o2 is int )
                        {
                            o = DataConvertor.ConvertToInt( o );
                            return o.Equals( o2 );
                        }
                        else if ( o is int && o2 is string )
                        {
                            o2 = DataConvertor.ConvertToInt( o2 );
                            return o.Equals( o2 );
                        }

                        throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                                            ResourceUtils.GetString( "NA1038" ),
                            GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ),
                            p0, p2 );
                    case TokenType.NE:
                        if ( o is string && o2 is string )
                        {
                            return !o.Equals( o2 );
                        }
                        else if ( o is bool && o2 is bool )
                        {
                            return !o.Equals( o2 );
                        }
                        else if ( o is int && o2 is int )
                        {
                            return !o.Equals( o2 );
                        }
                        else if ( o is int && o2 is long )
                        {
                            return !( Convert.ToInt64( o ) ).Equals( o2 );
                        }
                        else if ( o is int && o2 is double )
                        {
                            return !( Convert.ToDouble( o ) ).Equals( o2 );
                        }
                        else if ( o is long && o2 is long )
                        {
                            return !o.Equals( o2 );
                        }
                        else if ( o is long && o2 is int )
                        {
                            return !( o.Equals( Convert.ToInt64( o2 ) ) );
                        }
                        else if ( o is long && o2 is double )
                        {
                            return !( Convert.ToDouble( o ) ).Equals( o2 );
                        }
                        else if ( o is double && o2 is double )
                        {
                            return !o.Equals( o2 );
                        }
                        else if ( o is double && o2 is int )
                        {
                            return !o.Equals( Convert.ToDouble( o2 ) );
                        }
                        else if ( o is double && o2 is long )
                        {
                            return !o.Equals( Convert.ToDouble( o2 ) );
                        }
                        else if ( o is DateTime && o2 is DateTime )
                        {
                            return !o.Equals( o2 );
                        }
                        else if ( o is TimeSpan && o2 is TimeSpan )
                        {
                            return !o.Equals( o2 );
                        }
                        else if ( o.GetType().IsEnum )
                        {
                            if ( o2 is string )
                            {
                                return !o.Equals( Enum.Parse( o.GetType(), (string)o2, false ) );
                            }
                            else
                            {
                                return !o.Equals( Enum.ToObject( o.GetType(), o2 ) );
                            }
                        }
                        else if ( o2.GetType().IsEnum )
                        {
                            if ( o is string )
                            {
                                return !o2.Equals( Enum.Parse( o2.GetType(), (string)o, false ) );
                            }
                            else
                            {
                                return !o2.Equals( Enum.ToObject( o2.GetType(), o ) );
                            }
                        }

                        // Compare with different types
                        else if ( o is string && o2 is int )
                        {
                            o = DataConvertor.ConvertToInt( o );
                            return !o.Equals( o2 );
                        }
                        else if ( o is int && o2 is string )
                        {
                            o2 = DataConvertor.ConvertToInt( o2 );
                            return !o.Equals( o2 );
                        }

                        throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                            ResourceUtils.GetString( "NA1042" ),
                            GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ),
                            p0, p2 );
                    case TokenType.LT:
                        if ( o is string && o2 is string )
                        {
                            return string.Compare( (string)o, (string)o2, false,
                                CultureInfo.InvariantCulture ) < 0;
                        }
                        else if ( o is bool && o2 is bool )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) < 0;
                        }
                        else if ( o is int && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) < 0;
                        }
                        else if ( o is int && o2 is long )
                        {
                            return ( (IComparable)Convert.ToInt64( o ) ).CompareTo( o2 ) < 0;
                        }
                        else if ( o is int && o2 is double )
                        {
                            return ( (IComparable)Convert.ToDouble( o ) ).CompareTo( o2 ) < 0;
                        }
                        else if ( o is long && o2 is long )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) < 0;
                        }
                        else if ( o is long && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToInt64( o2 ) ) < 0;
                        }
                        else if ( o is long && o2 is double )
                        {
                            return ( (IComparable)Convert.ToDouble( o ) ).CompareTo( o2 ) < 0;
                        }
                        else if ( o is double && o2 is double )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) < 0;
                        }
                        else if ( o is double && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToDouble( o2 ) ) < 0;
                        }
                        else if ( o is double && o2 is long )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToDouble( o2 ) ) < 0;
                        }
                        else if ( o is DateTime && o2 is DateTime )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) < 0;
                        }
                        else if ( o is TimeSpan && o2 is TimeSpan )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) < 0;
                        }

                        // Compare with different types
                        else if ( o is string && o2 is int )
                        {
                            o = DataConvertor.ConvertToInt( o );
                            return ( (IComparable)o ).CompareTo( o2 ) < 0;
                        }
                        else if ( o is int && o2 is string )
                        {
                            o2 = DataConvertor.ConvertToInt( o2 );
                            return ( (IComparable)o ).CompareTo( o2 ) < 0;
                        }

                        throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                            ResourceUtils.GetString( "NA1051" ),
                            GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ),
                            p0, p2 );
                    case TokenType.GT:
                        if ( o is string && o2 is string )
                        {
                            return string.Compare( (string)o, (string)o2, false,
                                CultureInfo.InvariantCulture ) > 0;
                        }
                        else if ( o is bool && o2 is bool )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) > 0;
                        }
                        else if ( o is int && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) > 0;
                        }
                        else if ( o is int && o2 is long )
                        {
                            return ( (IComparable)Convert.ToInt64( o ) ).CompareTo( o2 ) > 0;
                        }
                        else if ( o is int && o2 is double )
                        {
                            return ( (IComparable)Convert.ToDouble( o ) ).CompareTo( o2 ) > 0;
                        }
                        else if ( o is long && o2 is long )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) > 0;
                        }
                        else if ( o is long && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToInt64( o2 ) ) > 0;
                        }
                        else if ( o is long && o2 is double )
                        {
                            return ( (IComparable)Convert.ToDouble( o ) ).CompareTo( o2 ) > 0;
                        }
                        else if ( o is double && o2 is double )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) > 0;
                        }
                        else if ( o is double && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToDouble( o2 ) ) > 0;
                        }
                        else if ( o is double && o2 is long )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToDouble( o2 ) ) > 0;
                        }
                        else if ( o is DateTime && o2 is DateTime )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) > 0;
                        }
                        else if ( o is TimeSpan && o2 is TimeSpan )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) > 0;
                        }

                        // Compare with different types
                        else if ( o is string && o2 is int )
                        {
                            o = DataConvertor.ConvertToInt( o );
                            return ( (IComparable)o ).CompareTo( o2 ) > 0;
                        }
                        else if ( o is int && o2 is string )
                        {
                            o2 = DataConvertor.ConvertToInt( o2 );
                            return ( (IComparable)o ).CompareTo( o2 ) > 0;
                        }

                        throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                            ResourceUtils.GetString( "NA1037" ),
                            GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ),
                            p0, p2 );
                    case TokenType.LE:
                        if ( o is string && o2 is string )
                        {
                            return string.Compare( (string)o, (string)o2, false,
                                CultureInfo.InvariantCulture ) <= 0;
                        }
                        else if ( o is bool && o2 is bool )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) <= 0;
                        }
                        else if ( o is int && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) <= 0;
                        }
                        else if ( o is int && o2 is long )
                        {
                            return ( (IComparable)Convert.ToInt64( o ) ).CompareTo( o2 ) <= 0;
                        }
                        else if ( o is int && o2 is double )
                        {
                            return ( (IComparable)Convert.ToDouble( o ) ).CompareTo( o2 ) <= 0;
                        }
                        else if ( o is long && o2 is long )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) <= 0;
                        }
                        else if ( o is long && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToInt64( o2 ) ) <= 0;
                        }
                        else if ( o is long && o2 is double )
                        {
                            return ( (IComparable)Convert.ToDouble( o ) ).CompareTo( o2 ) <= 0;
                        }
                        else if ( o is double && o2 is double )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) <= 0;
                        }
                        else if ( o is double && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToDouble( o2 ) ) <= 0;
                        }
                        else if ( o is double && o2 is long )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToDouble( o2 ) ) <= 0;
                        }
                        else if ( o is DateTime && o2 is DateTime )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) <= 0;
                        }
                        else if ( o is TimeSpan && o2 is TimeSpan )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) <= 0;
                        }

                        // Compare with different types
                        else if ( o is string && o2 is int )
                        {
                            o = DataConvertor.ConvertToInt( o );
                            return ( (IComparable)o ).CompareTo( o2 ) <= 0;
                        }
                        else if ( o is int && o2 is string )
                        {
                            o2 = DataConvertor.ConvertToInt( o2 );
                            return ( (IComparable)o ).CompareTo( o2 ) <= 0;
                        }

                        throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                            ResourceUtils.GetString( "NA1049" ),
                            GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ),
                            p0, p2 );
                    case TokenType.GE:
                        if ( o is string && o2 is string )
                        {
                            return string.Compare( (string)o, (string)o2, false,
                                CultureInfo.InvariantCulture ) >= 0;
                        }
                        else if ( o is bool && o2 is bool )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) >= 0;
                        }
                        else if ( o is int && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) >= 0;
                        }
                        else if ( o is int && o2 is long )
                        {
                            return ( (IComparable)Convert.ToInt64( o ) ).CompareTo( o2 ) >= 0;
                        }
                        else if ( o is int && o2 is double )
                        {
                            return ( (IComparable)Convert.ToDouble( o ) ).CompareTo( o2 ) >= 0;
                        }
                        else if ( o is long && o2 is long )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) >= 0;
                        }
                        else if ( o is long && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToInt64( o2 ) ) >= 0;
                        }
                        else if ( o is long && o2 is double )
                        {
                            return ( (IComparable)Convert.ToDouble( o ) ).CompareTo( o2 ) >= 0;
                        }
                        else if ( o is double && o2 is double )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) >= 0;
                        }
                        else if ( o is double && o2 is int )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToDouble( o2 ) ) >= 0;
                        }
                        else if ( o is double && o2 is long )
                        {
                            return ( (IComparable)o ).CompareTo( Convert.ToDouble( o2 ) ) >= 0;
                        }
                        else if ( o is DateTime && o2 is DateTime )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) >= 0;
                        }
                        else if ( o is TimeSpan && o2 is TimeSpan )
                        {
                            return ( (IComparable)o ).CompareTo( o2 ) >= 0;
                        }

                        // Compare with different types
                        else if ( o is string && o2 is int )
                        {
                            o = DataConvertor.ConvertToInt( o );
                            return ( (IComparable)o ).CompareTo( o2 ) >= 0;
                        }
                        else if ( o is int && o2 is string )
                        {
                            o2 = DataConvertor.ConvertToInt( o2 );
                            return ( (IComparable)o ).CompareTo( o2 ) >= 0;
                        }

                        throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                            ResourceUtils.GetString( "NA1050" ),
                            GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ),
                            p0, p2 );
                }
            }
            return o;
        }

        private object ParseAddSubtract()
        {
            Position p0 = _tokenizer.CurrentPosition;
            object o = ParseMulDiv();

            while ( true )
            {
                if ( _tokenizer.CurrentToken == TokenType.Plus )
                {
                    _tokenizer.GetNextToken();
                    object o2 = ParseMulDiv();
                    Position p3 = _tokenizer.CurrentPosition;

                    if ( !SyntaxCheckOnly() )
                    {
                        if ( o is string && o2 is string )
                        {
                            o = (string)o + (string)o2;
                        }
                        else if ( o is int && o2 is int )
                        {
                            o = (int)o + (int)o2;
                        }
                        else if ( o is int && o2 is long )
                        {
                            o = (int)o + (long)o2;
                        }
                        else if ( o is int && o2 is double )
                        {
                            o = (int)o + (double)o2;
                        }
                        else if ( o is long && o2 is long )
                        {
                            o = (long)o + (long)o2;
                        }
                        else if ( o is long && o2 is int )
                        {
                            o = (long)o + (int)o2;
                        }
                        else if ( o is long && o2 is double )
                        {
                            o = (long)o + (double)o2;
                        }
                        else if ( o is double && o2 is double )
                        {
                            o = (double)o + (double)o2;
                        }
                        else if ( o is double && o2 is int )
                        {
                            o = (double)o + (int)o2;
                        }
                        else if ( o is double && o2 is long )
                        {
                            o = (double)o + (long)o2;
                        }
                        else if ( o is DateTime && o2 is TimeSpan )
                        {
                            o = (DateTime)o + (TimeSpan)o2;
                        }
                        else if ( o is TimeSpan && o2 is TimeSpan )
                        {
                            o = (TimeSpan)o + (TimeSpan)o2;
                        }
                        else
                        {
                            throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                ResourceUtils.GetString( "NA1041" ),
                                GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ),
                                p0, p3 );
                        }
                    }
                }
                else if ( _tokenizer.CurrentToken == TokenType.Minus )
                {
                    _tokenizer.GetNextToken();

                    object o2 = ParseMulDiv();
                    Position p3 = _tokenizer.CurrentPosition;

                    if ( !SyntaxCheckOnly() )
                    {
                        if ( o is int && o2 is int )
                        {
                            o = (int)o - (int)o2;
                        }
                        else if ( o is int && o2 is long )
                        {
                            o = (int)o - (long)o2;
                        }
                        else if ( o is int && o2 is double )
                        {
                            o = (int)o - (double)o2;
                        }
                        else if ( o is long && o2 is long )
                        {
                            o = (long)o - (long)o2;
                        }
                        else if ( o is long && o2 is int )
                        {
                            o = (long)o - (int)o2;
                        }
                        else if ( o is long && o2 is double )
                        {
                            o = (long)o - (double)o2;
                        }
                        else if ( o is double && o2 is double )
                        {
                            o = (double)o - (double)o2;
                        }
                        else if ( o is double && o2 is int )
                        {
                            o = (double)o - (int)o2;
                        }
                        else if ( o is double && o2 is long )
                        {
                            o = (double)o - (long)o2;
                        }
                        else if ( o is DateTime && o2 is DateTime )
                        {
                            o = (DateTime)o - (DateTime)o2;
                        }
                        else if ( o is DateTime && o2 is TimeSpan )
                        {
                            o = (DateTime)o - (TimeSpan)o2;
                        }
                        else if ( o is TimeSpan && o2 is TimeSpan )
                        {
                            o = (TimeSpan)o - (TimeSpan)o2;
                        }
                        else
                        {
                            throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                ResourceUtils.GetString( "NA1048" ),
                                GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ),
                                p0, p3 );
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return o;
        }

        private object ParseMulDiv()
        {
            Position p0 = _tokenizer.CurrentPosition;
            object o = ParseValue();

            while ( true )
            {
                if ( _tokenizer.CurrentToken == TokenType.Mul )
                {
                    _tokenizer.GetNextToken();

                    object o2 = ParseValue();
                    Position p3 = _tokenizer.CurrentPosition;

                    if ( !SyntaxCheckOnly() )
                    {
                        if ( o is int && o2 is int )
                        {
                            o = (int)o * (int)o2;
                        }
                        else if ( o is int && o2 is long )
                        {
                            o = (int)o * (long)o2;
                        }
                        else if ( o is int && o2 is double )
                        {
                            o = (int)o * (double)o2;
                        }
                        else if ( o is long && o2 is long )
                        {
                            o = (long)o * (long)o2;
                        }
                        else if ( o is long && o2 is int )
                        {
                            o = (long)o * (int)o2;
                        }
                        else if ( o is long && o2 is double )
                        {
                            o = (long)o * (double)o2;
                        }
                        else if ( o is double && o2 is double )
                        {
                            o = (double)o * (double)o2;
                        }
                        else if ( o is double && o2 is int )
                        {
                            o = (double)o * (int)o2;
                        }
                        else if ( o is double && o2 is long )
                        {
                            o = (double)o * (long)o2;
                        }
                        else
                        {
                            throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                        ResourceUtils.GetString( "NA1036" ),
                                        GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ), p0, p3 );
                        }
                    }
                }
                else if ( _tokenizer.CurrentToken == TokenType.Div )
                {
                    _tokenizer.GetNextToken();

                    Position p2 = _tokenizer.CurrentPosition;
                    object o2 = ParseValue();
                    Position p3 = _tokenizer.CurrentPosition;

                    if ( !SyntaxCheckOnly() )
                    {
                        if ( o is int && o2 is int )
                        {
                            if ( (int)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (int)o / (int)o2;
                        }
                        else if ( o is int && o2 is long )
                        {
                            if ( (long)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (int)o / (long)o2;
                        }
                        else if ( o is int && o2 is double )
                        {
                            if ( (double)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (int)o / (double)o2;
                        }
                        else if ( o is long && o2 is long )
                        {
                            if ( (long)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (long)o / (long)o2;
                        }
                        else if ( o is long && o2 is int )
                        {
                            if ( (int)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (long)o / (int)o2;
                        }
                        else if ( o is long && o2 is double )
                        {
                            if ( (double)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (long)o / (double)o2;
                        }
                        else if ( o is double && o2 is double )
                        {
                            if ( (double)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (double)o / (double)o2;
                        }
                        else if ( o is double && o2 is int )
                        {
                            if ( (int)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (double)o / (int)o2;
                        }
                        else if ( o is double && o2 is long )
                        {
                            if ( (long)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (double)o / (long)o2;
                        }
                        else
                        {
                            throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                ResourceUtils.GetString( "NA1039" ),
                                GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ), p0, p3 );
                        }
                    }
                }
                else if ( _tokenizer.CurrentToken == TokenType.Mod )
                {
                    _tokenizer.GetNextToken();

                    Position p2 = _tokenizer.CurrentPosition;
                    object o2 = ParseValue();
                    Position p3 = _tokenizer.CurrentPosition;

                    if ( !SyntaxCheckOnly() )
                    {
                        if ( o is int && o2 is int )
                        {
                            if ( (int)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (int)o % (int)o2;
                        }
                        else if ( o is int && o2 is long )
                        {
                            if ( (long)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (int)o % (long)o2;
                        }
                        else if ( o is int && o2 is double )
                        {
                            if ( (double)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (int)o % (double)o2;
                        }
                        else if ( o is long && o2 is long )
                        {
                            if ( (long)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (long)o % (long)o2;
                        }
                        else if ( o is long && o2 is int )
                        {
                            if ( (int)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (long)o % (int)o2;
                        }
                        else if ( o is long && o2 is double )
                        {
                            if ( (double)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (long)o % (double)o2;
                        }
                        else if ( o is double && o2 is double )
                        {
                            if ( (double)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (double)o % (double)o2;
                        }
                        else if ( o is double && o2 is int )
                        {
                            if ( (int)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (double)o % (int)o2;
                        }
                        else if ( o is double && o2 is long )
                        {
                            if ( (long)o2 == 0 )
                            {
                                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1043" ) ), p2, p3 );
                            }

                            o = (double)o % (long)o2;
                        }
                        else
                        {
                            throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                ResourceUtils.GetString( "NA1047" ),
                                GetSimpleTypeName( o.GetType() ), GetSimpleTypeName( o2.GetType() ) ), p0, p3 );
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return o;
        }

        private object ParseConditional()
        {
            // we're on "if" token - skip it 
            _tokenizer.GetNextToken();
            if ( _tokenizer.CurrentToken != TokenType.LeftParen )
            {
                throw BuildParseError( "'(' expected.", _tokenizer.CurrentPosition );
            }
            _tokenizer.GetNextToken();


            Position p0 = _tokenizer.CurrentPosition;
            object val = ParseExpression();
            Position p1 = _tokenizer.CurrentPosition;

            bool cond = false;
            if ( !SyntaxCheckOnly() )
            {
                cond = (bool)SafeConvert( typeof( bool ), val, "the conditional expression", p0, p1 );
            }

            // skip comma between condition value and then
            if ( _tokenizer.CurrentToken != TokenType.Comma )
            {
                throw BuildParseError( "',' expected.", _tokenizer.CurrentPosition );
            }
            _tokenizer.GetNextToken();

            EvalMode oldEvalMode = _evalMode;

            try
            {
                if ( !cond )
                {
                    // evaluate 'then' clause without executing functions
                    _evalMode = EvalMode.ParseOnly;
                }
                else
                {
                    _evalMode = oldEvalMode;
                }
                object thenValue = ParseExpression();
                _evalMode = oldEvalMode;

                if ( _tokenizer.CurrentToken != TokenType.Comma )
                {
                    throw BuildParseError( "',' expected.", _tokenizer.CurrentPosition );
                }
                _tokenizer.GetNextToken(); // skip comma

                if ( cond )
                {
                    // evaluate 'else' clause without executing functions
                    _evalMode = EvalMode.ParseOnly;
                }
                else
                {
                    _evalMode = oldEvalMode;
                }
                object elseValue = ParseExpression();

                _evalMode = oldEvalMode;

                // skip closing ')'
                if ( _tokenizer.CurrentToken != TokenType.RightParen )
                {
                    throw BuildParseError( "')' expected.", _tokenizer.CurrentPosition );
                }
                _tokenizer.GetNextToken();

                return cond ? thenValue : elseValue;
            }
            finally
            {
                // restore evaluation mode - even on exceptions
                _evalMode = oldEvalMode;
            }
        }

        private object ParseValue()
        {
            if ( _tokenizer.CurrentToken == TokenType.String )
            {
                object v = _tokenizer.TokenText;
                _tokenizer.GetNextToken();
                return v;
            }

            if ( _tokenizer.CurrentToken == TokenType.Number )
            {
                string number = _tokenizer.TokenText;

                Position p0 = _tokenizer.CurrentPosition;
                _tokenizer.GetNextToken();
                Position p1 = new Position(
                    _tokenizer.CurrentPosition.CharIndex - 1 );

                if ( _tokenizer.CurrentToken == TokenType.Dot )
                {
                    number += ".";
                    _tokenizer.GetNextToken();
                    if ( _tokenizer.CurrentToken != TokenType.Number )
                    {
                        throw BuildParseError( "Fractional part expected.", _tokenizer.CurrentPosition );
                    }
                    number += _tokenizer.TokenText;

                    _tokenizer.GetNextToken();

                    p1 = _tokenizer.CurrentPosition;

                    try
                    {
                        return Double.Parse( number, CultureInfo.InvariantCulture );
                    }
                    catch ( OverflowException )
                    {
                        throw BuildParseError( "Value was either too large or too"
                            + " small for type 'double'.", p0, p1 );
                    }
                }
                else
                {
                    try
                    {
                        return Int32.Parse( number, CultureInfo.InvariantCulture );
                    }
                    catch ( OverflowException )
                    {
                        try
                        {
                            return long.Parse( number, CultureInfo.InvariantCulture );
                        }
                        catch ( OverflowException )
                        {
                            throw BuildParseError( "Value was either too large or too"
                                + " small for type 'long'.", p0, p1 );
                        }
                    }
                }
            }

            if ( _tokenizer.CurrentToken == TokenType.Minus )
            {
                _tokenizer.GetNextToken();

                // unary minus
                Position p0 = _tokenizer.CurrentPosition;
                object v = ParseValue();
                Position p1 = _tokenizer.CurrentPosition;
                if ( !SyntaxCheckOnly() )
                {
                    if ( v is int )
                    {
                        return -( (int)v );
                    }
                    if ( v is long )
                    {
                        return -( (long)v );
                    }
                    if ( v is double )
                    {
                        return -( (double)v );
                    }
                    throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                        ResourceUtils.GetString( "NA1040" ),
                        GetSimpleTypeName( v.GetType() ) ), p0, p1 );
                }
                return null;
            }

            if ( _tokenizer.IsKeyword( "not" ) )
            {
                _tokenizer.GetNextToken();

                // unary boolean not
                Position p0 = _tokenizer.CurrentPosition;
                object v = ParseValue();
                Position p1 = _tokenizer.CurrentPosition;
                if ( !SyntaxCheckOnly() )
                {
                    bool value = (bool)SafeConvert( typeof( bool ), v, "the argument of 'not' operator", p0, p1 );
                    return !value;
                }
                return null;
            }

            if ( _tokenizer.CurrentToken == TokenType.LeftParen )
            {
                _tokenizer.GetNextToken();
                object v = ParseExpression();
                if ( _tokenizer.CurrentToken != TokenType.RightParen )
                {
                    throw BuildParseError( "')' expected.", _tokenizer.CurrentPosition );
                }
                _tokenizer.GetNextToken();
                return v;
            }

            if ( _tokenizer.CurrentToken == TokenType.Keyword )
            {
                Position p0 = _tokenizer.CurrentPosition;

                string functionOrPropertyName = _tokenizer.TokenText;
                if ( functionOrPropertyName == "if" )
                {
                    return ParseConditional();
                }

                if ( functionOrPropertyName == "true" )
                {
                    _tokenizer.GetNextToken();
                    return true;
                }

                if ( functionOrPropertyName == "false" )
                {
                    _tokenizer.GetNextToken();
                    return false;
                }

                // don't ignore whitespace - properties shouldn't be written with spaces in them

                _tokenizer.IgnoreWhitespace = false;
                _tokenizer.GetNextToken();

                ArrayList args = new ArrayList();
                bool isFunction = false;

                // gather function or property name
                if ( _tokenizer.CurrentToken == TokenType.DoubleColon )
                {
                    isFunction = true;
                    functionOrPropertyName += "::";
                    _tokenizer.GetNextToken();
                    if ( _tokenizer.CurrentToken != TokenType.Keyword )
                    {
                        throw BuildParseError( "Function name expected.", p0, _tokenizer.CurrentPosition );
                    }
                    functionOrPropertyName += _tokenizer.TokenText;
                    _tokenizer.GetNextToken();
                }
                else
                {
                    while ( _tokenizer.CurrentToken == TokenType.Dot
                            || _tokenizer.CurrentToken == TokenType.Minus
                            || _tokenizer.CurrentToken == TokenType.Keyword
                            || _tokenizer.CurrentToken == TokenType.Number )
                    {
                        functionOrPropertyName += _tokenizer.TokenText;
                        _tokenizer.GetNextToken();
                    }
                }
                _tokenizer.IgnoreWhitespace = true;

                // if we've stopped on a whitespace - advance to the next token
                if ( _tokenizer.CurrentToken == TokenType.Whitespace )
                {
                    _tokenizer.GetNextToken();
                }

                if ( isFunction )
                {
                    if ( _tokenizer.CurrentToken != TokenType.LeftParen )
                    {
                        throw BuildParseError( "'(' expected.", _tokenizer.CurrentPosition );
                    }

                    _tokenizer.GetNextToken();

                    int currentArgument = 0;
                    ParameterInfo[] formalParameters = null;

                    try
                    {
                        formalParameters = GetFunctionParameters( functionOrPropertyName );
                    }
                    catch ( Exception e )
                    {
                        throw BuildParseError( e.Message, p0, _tokenizer.CurrentPosition );
                    }

                    while ( _tokenizer.CurrentToken != TokenType.RightParen &&
                            _tokenizer.CurrentToken != TokenType.EOF )
                    {
                        if ( currentArgument >= formalParameters.Length )
                        {
                            throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                        ResourceUtils.GetString( "NA1046" ), functionOrPropertyName ), p0, _tokenizer.CurrentPosition );
                        }

                        Position beforeArgument = _tokenizer.CurrentPosition;
                        object e = ParseExpression();
                        Position afterArgument = _tokenizer.CurrentPosition;

                        if ( !SyntaxCheckOnly() )
                        {
                            object convertedValue = SafeConvert( formalParameters[ currentArgument ].ParameterType,
                                    e,
                                    string.Format( CultureInfo.InvariantCulture, "argument {1} ({0}) of {2}()", formalParameters[ currentArgument ].Name, currentArgument + 1, functionOrPropertyName ),
                                    beforeArgument, afterArgument );
                            args.Add( convertedValue );
                        }
                        currentArgument++;
                        if ( _tokenizer.CurrentToken == TokenType.RightParen )
                        {
                            break;
                        }
                        if ( _tokenizer.CurrentToken != TokenType.Comma )
                        {
                            throw BuildParseError( "',' expected.", _tokenizer.CurrentPosition );
                        }
                        _tokenizer.GetNextToken();
                    }
                    if ( currentArgument < formalParameters.Length )
                    {
                        throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                                    ResourceUtils.GetString( "NA1044" ), functionOrPropertyName ), p0, _tokenizer.CurrentPosition );
                    }

                    if ( _tokenizer.CurrentToken != TokenType.RightParen )
                    {
                        throw BuildParseError( "')' expected.", _tokenizer.CurrentPosition );
                    }
                    _tokenizer.GetNextToken();
                }

                try
                {
                    if ( !SyntaxCheckOnly() )
                    {
                        if ( isFunction )
                        {
                            return EvaluateFunction( functionOrPropertyName, args.ToArray() );
                        }
                        else
                        {
                            return EvaluateProperty( functionOrPropertyName );
                        }
                    }
                    else
                    {
                        return null; // this is needed because of short-circuit evaluation
                    }
                }
                catch ( Exception e )
                {
                    if ( isFunction )
                    {
                        throw BuildParseError( "Function call failed.", p0, _tokenizer.CurrentPosition, e );
                    }
                    else
                    {
                        throw BuildParseError( "Property evaluation failed.", p0, _tokenizer.CurrentPosition, e );
                    }
                }
            }

            return UnexpectedToken();
        }

        protected ExpressionParseException BuildParseError( string desc, Position p0 )
        {
            return new ExpressionParseException( desc, p0.CharIndex );
        }

        protected ExpressionParseException BuildParseError( string desc, Position p0, Position p1 )
        {
            return new ExpressionParseException( desc, p0.CharIndex, p1.CharIndex );
        }

        protected ExpressionParseException BuildParseError( string desc, Position p0, Position p1, Exception ex )
        {
            return new ExpressionParseException( desc, p0.CharIndex, p1.CharIndex, ex );
        }

        protected object SafeConvert( Type returnType, object source, string description, Position p0, Position p1 )
        {
            try
            {
                //
                // TODO - Convert.ChangeType() is very liberal. It allows you to convert "true" to Double (1.0).
                // We shouldn't allow this. Add more cases like this here.
                //
                bool disallow = false;

                if ( source == null )
                {
                    if ( returnType == typeof( string ) )
                    {
                        return string.Empty;
                    }

                    throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                        ResourceUtils.GetString( "NA1045" ),
                        description, GetSimpleTypeName( returnType ) ), p0, p1 );
                }

                if ( source is bool )
                {
                    if ( returnType != typeof( string ) && returnType != typeof( bool ) )
                    {
                        // boolean can only be converted to string or boolean
                        disallow = true;
                    }
                }

                if ( returnType == typeof( bool ) )
                {
                    if ( !( source is string || source is bool ) )
                    {
                        // only string and boolean can be converted to boolean
                        disallow = true;
                    }
                }

                if ( source is DateTime )
                {
                    if ( returnType != typeof( string ) && returnType != typeof( DateTime ) )
                    {
                        // DateTime can only be converted to string or DateTime
                        disallow = true;
                    }
                }

                if ( returnType == typeof( DateTime ) )
                {
                    if ( !( source is DateTime || source is string ) )
                    {
                        // only string and DateTime can be converted to DateTime
                        disallow = true;
                    }
                }

                if ( source is TimeSpan && returnType != typeof( TimeSpan ) )
                {
                    // implicit conversion from TimeSpan is not supported, as
                    // TimeSpan does not implement IConvertible
                    disallow = true;
                }

                if ( returnType == typeof( TimeSpan ) && !( source is TimeSpan ) )
                {
                    // implicit conversion to TimeSpan is not supported
                    disallow = true;
                }

                if ( returnType == typeof( string ) )
                {
                    if ( source is DirectoryInfo )
                    {
                        return ( (DirectoryInfo)source ).FullName;
                    }
                    else if ( source is FileInfo )
                    {
                        return ( (FileInfo)source ).FullName;
                    }
                }

                if ( returnType.IsEnum )
                {
                    if ( source is string )
                    {
                        return Enum.Parse( returnType, (string)source, false );
                    }
                    else
                    {
                        return Enum.ToObject( returnType, source );
                    }
                }

                if ( disallow )
                {
                    throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                        "Cannot convert {0} to '{1}' (actual type was '{2}').",
                        description, GetSimpleTypeName( returnType ),
                        GetSimpleTypeName( source.GetType() ) ), p0, p1 );
                }

                if ( returnType.IsAssignableFrom( source.GetType() ) )
                {
                    return source;
                }

                return Convert.ChangeType( source, returnType, CultureInfo.InvariantCulture );
            }
            catch ( ExpressionParseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                    "Cannot convert {0} to '{1}' (actual type was '{2}').",
                    description, GetSimpleTypeName( returnType ),
                    GetSimpleTypeName( source.GetType() ) ), p0, p1, ex );
            }
        }

        protected string GetSimpleTypeName( Type t )
        {
            if ( t == typeof( int ) )
            {
                return "int";
            }
            else if ( t == typeof( long ) )
            {
                return "long";
            }
            else if ( t == typeof( double ) )
            {
                return "double";
            }
            else if ( t == typeof( string ) )
            {
                return "string";
            }
            else if ( t == typeof( bool ) )
            {
                return "bool";
            }
            else if ( t == typeof( DateTime ) )
            {
                return "datetime";
            }
            else if ( t == typeof( TimeSpan ) )
            {
                return "timespan";
            }
            else
            {
                return t.FullName;
            }
        }

        #endregion Parser

        #region Overridables

        protected abstract object EvaluateFunction( string functionName, object[] args );
        protected abstract ParameterInfo[] GetFunctionParameters( string functionName );
        protected abstract object EvaluateProperty( string propertyName );

        protected virtual object UnexpectedToken()
        {
            throw BuildParseError( string.Format( CultureInfo.InvariantCulture,
                "Unexpected token '{0}'.", _tokenizer.CurrentToken ), _tokenizer.CurrentPosition );
        }

        #endregion Overridables
    }
}
