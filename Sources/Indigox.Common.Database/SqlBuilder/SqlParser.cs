using System;
using System.Collections.Generic;
using Indigox.Common.Logging;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlParser
    {
        public static string[] ParseTokens( string exp )
        {
            List<string> tokens = new List<string>();
            string token;
            char ch;
            bool tokenBegin = false;
            int tokenBeginIndex = -1;
            for ( int i = 0; i < exp.Length; i++ )
            {
                ch = exp[ i ];
                if ( OpenChars.Contains( ch ) )
                {
                    if ( tokenBegin )
                    {
                        token = exp.Substring( tokenBeginIndex, i - tokenBeginIndex );
                        tokenBeginIndex = -1;
                        tokenBegin = false;
                        tokens.Add( token );
                    }
                    token = GetClosedToken( exp, i, ch );
                    tokens.Add( token );
                    i = i + token.Length - 1;
                }
                else if ( Quots.Contains( ch ) )
                {
                    if ( tokenBegin )
                    {
                        token = exp.Substring( tokenBeginIndex, i - tokenBeginIndex );
                        tokenBeginIndex = -1;
                        tokenBegin = false;
                        tokens.Add( token );
                    }
                    token = GetQoutedToken( exp, i, ch );
                    tokens.Add( token );
                    i = i + token.Length - 1;
                }
                else if ( OperateChars.Contains( ch ) )
                {
                    if ( tokenBegin )
                    {
                        token = exp.Substring( tokenBeginIndex, i - tokenBeginIndex );
                        tokenBeginIndex = -1;
                        tokenBegin = false;
                        tokens.Add( token );
                    }
                    token = GetOperateToken( exp, i );
                    tokens.Add( token );
                    i = i + token.Length - 1;
                }
                else if ( EmptyChars.Contains( ch ) )
                {
                    if ( tokenBegin )
                    {
                        token = exp.Substring( tokenBeginIndex, i - tokenBeginIndex );
                        tokenBeginIndex = -1;
                        tokenBegin = false;
                        tokens.Add( token );
                    }
                }
                else if ( !tokenBegin )
                {
                    tokenBeginIndex = i;
                    tokenBegin = true;
                }
            }
            if ( tokenBegin )
            {
                token = exp.Substring( tokenBeginIndex );
                tokenBeginIndex = -1;
                tokenBegin = false;
                tokens.Add( token );
            }
            return tokens.ToArray();
        }

        private static string GetQoutedToken( string exp, int index, char quot )
        {
            char ch;
            for ( int i = index + 1; i < exp.Length; i++ )
            {
                ch = exp[ i ];
                if ( ch == quot )
                {
                    if ( ( i + 1 ) < exp.Length && exp[ i + 1 ] == quot )
                    {
                        i++;
                    }
                    else
                    {
                        return exp.Substring( index, i - index + 1 );
                    }
                }
            }
            return exp.Substring( index );
        }

        private static string GetOperateToken( string exp, int index )
        {
            char ch;
            for ( int i = index + 1; i < exp.Length; i++ )
            {
                ch = exp[ i ];
                if ( !OperateChars.Contains( ch ) )
                {
                    return exp.Substring( index, i - index );
                }
            }
            return exp.Substring( index );
        }

        private static string GetClosedToken( string exp, int index, char openChar )
        {
            char ch, closeChar = CloseChars[ OpenChars.IndexOf( openChar ) ];
            int lastOpenCharCount = 1;
            for ( int i = index + 1; i < exp.Length; i++ )
            {
                ch = exp[ i ];
                if ( ch == openChar )
                {
                    lastOpenCharCount++;
                }
                if ( ch == closeChar )
                {
                    lastOpenCharCount--;
                    if ( lastOpenCharCount == 0 )
                    {
                        return exp.Substring( index, i - index + 1 );
                    }
                }
            }
            throw new ApplicationException( "expect '" + closeChar + "' after \"" + exp.Substring( index, Math.Min( exp.Length - index, 5 ) ) + "...\" at position " + index );
        }

        private static readonly List<char> OpenChars = new List<char>( new char[] { '[', '(' } );
        private static readonly List<char> CloseChars = new List<char>( new char[] { ']', ')' } );
        private static readonly List<char> Quots = new List<char>( new char[] { '\'', '\"' } );
        private static readonly List<char> OperateChars = new List<char>( new char[] { '+', '-', '*', '/', '=', '>', '<' } );
        private static readonly List<char> EmptyChars = new List<char>( new char[] { ' ', '\t', '\n', '\r' } );
    }
}