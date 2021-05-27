using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Expression
{
    public enum TokenType
    {
        BOF,
        EOF,
        Number,
        String,
        Keyword,
        EQ,
        NE,
        LT,
        GT,
        LE,
        GE,
        Plus,
        Minus,
        Mul,
        Div,
        Mod,
        LeftParen,
        RightParen,
        LeftCurlyBrace,
        RightCurlyBrace,
        Not,
        Punctuation,
        Whitespace,
        Dollar,
        Comma,
        Dot,
        DoubleColon,
    }
}
