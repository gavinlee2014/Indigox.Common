using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Expression.Interface
{
    public interface IExpressionTokenizer
    {
        Position CurrentPosition { get; }
        TokenType CurrentToken { get; }
        string TokenText { get; }
        void InitTokenizer( string s );
        void GetNextToken();
        bool IsKeyword( string k );
        bool IgnoreWhitespace { get; set; }
        bool SingleCharacterMode { get; set; }
    }
}
