using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Indigox.Common.Membership.FileImpl.Caches
{
    internal class CsvFileReader : IDisposable
    {
        private TextReader reader;
        private string filename;
        private bool hasHeaderLine = true;

        private static readonly char[] column_seperator = new char[] { '\t' };
        private static readonly string nullString = "\0";

        private Dictionary<string, int> columns = new Dictionary<string, int>( StringComparer.CurrentCultureIgnoreCase );

        public CsvFileReader( string filename )
        {
            this.filename = filename;
            this.reader = new StreamReader( filename, Encoding.Default );
            if ( this.hasHeaderLine )
            {
                string headerLine = this.reader.ReadLine();
                string[] columnNames = headerLine.Split( column_seperator );
                for ( int i = 0; i < columnNames.Length; i++ )
                {
                    columns.Add( columnNames[ i ], i );
                }
            }
        }

        public int GetOrdinal( string columnName )
        {
            return columns[ columnName ];
        }

        public string[] Read()
        {
            string line = reader.ReadLine();

            if ( string.IsNullOrEmpty( line ) )
            {
                return null;
            }

            string[] cols = line.Split( column_seperator );

            for ( int i = 0; i < cols.Length; i++ )
            {
                if ( cols[ i ] == nullString )
                {
                    cols[ i ] = null;
                }
            }

            return cols;
        }

        public void Dispose()
        {
            this.reader.Close();
        }
    }
}