using System;
using System.IO;

namespace Indigox.Common.Statistics.Exporters
{
    internal class CsvWriter : IDisposable
    {
        private const string COMMA = ",";
        private const string EXP = "~";
        private const string DQUOTE = "\"";
        private const string DDQUOTE = "\"\"";
        private const string CRLF = "\r\n";
        private const string CR = "\r";
        private const string LF = "\n";
        private const string SPACE = "\x20";
        private const string PIPE = "\x7C";
        private const string TAB = "\x09";

        private const string EOR = CRLF;
        private const string EOF = CRLF;
        private const string SEPARATOR = COMMA;

        private TextWriter innerWriter;
        private WriterState state = WriterState.FirstCell;

        public CsvWriter( TextWriter writer )
        {
            this.innerWriter = writer;
        }

        public void WriteCell( object value )
        {
            if ( value == null || value == DBNull.Value )
            {
                WriteTextCell( null );
            }
            else if ( value is int )
            {
                WriteCell( (int)value );
            }
            else if ( value is long )
            {
                WriteCell( (long)value );
            }
            else if ( value is bool )
            {
                WriteCell( (bool)value );
            }
            else if ( value is double )
            {
                WriteCell( (double)value );
            }
            else if ( value is DateTime )
            {
                WriteCell( (DateTime)value );
            }
            else if ( value is Guid )
            {
                WriteCell( (Guid)value );
            }
            else
            {
                WriteTextCell( value.ToString() );
            }
        }

        public void WriteCell( int value )
        {
            WriteTextCell( value.ToString() );
        }

        public void WriteCell( long value )
        {
            WriteTextCell( value.ToString() );
        }

        public void WriteCell( bool value )
        {
            WriteTextCell( value ? "True" : "False" );
        }

        public void WriteCell( double value )
        {
            WriteTextCell( value.ToString( "R" ) );
        }

        public void WriteCell( DateTime value )
        {
            WriteTextCell( value.ToString( "yyyy-MM-dd HH:mm:ss" ) );
        }

        public void WriteCell( Guid value )
        {
            WriteTextCell( value.ToString( "B" ) );
        }

        public void WriteCell( string value )
        {
            WriteTextCell( value );
        }

        private void WriteTextCell( string value )
        {
            if ( this.state == WriterState.Cell )
            {
                this.innerWriter.Write( SEPARATOR );
            }

            if ( value != null )
            {
                bool containsCRLF = value.Contains( CRLF );
                bool containsComma = value.Contains( COMMA );
                bool containsDoubleQuote = value.Contains( DQUOTE );

                bool needDoubleQuoteProtection = ( containsCRLF || containsDoubleQuote || containsComma );

                if ( needDoubleQuoteProtection )
                {
                    this.innerWriter.Write( DQUOTE );
                }

                if ( containsCRLF )
                {
                    value = value.Replace( CRLF, LF );
                }

                if ( containsDoubleQuote )
                {
                    value = value.Replace( DQUOTE, DDQUOTE );
                }

                this.innerWriter.Write( value );

                if ( needDoubleQuoteProtection )
                {
                    this.innerWriter.Write( DQUOTE );
                }
            }

            this.state = WriterState.Cell;
        }

        public void WriteNewRow()
        {
            this.innerWriter.Write( EOR );
            this.state = WriterState.FirstCell;
        }

        private enum WriterState
        {
            FirstCell,
            Cell
        }

        public void Dispose()
        {
            if ( this.state != WriterState.FirstCell )
            {
                this.innerWriter.Write( EOF );
                this.state = WriterState.FirstCell;
            }
        }
    }
}