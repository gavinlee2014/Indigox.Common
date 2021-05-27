using System;
using System.IO;
using System.Text;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Statistics.Exporters
{
    public class CsvExporter
    {
        private FileInfo outputFile;
        private TextWriter txtWriter;

        private CsvWriter writer;

        public CsvExporter( string filename )
        {
            this.outputFile = new FileInfo( filename );
        }

        public CsvExporter( FileInfo outputFile )
        {
            this.outputFile = outputFile;
        }

        public CsvExporter( TextWriter txtWriter )
        {
            this.txtWriter = txtWriter;
        }

        public void Export( IRecordSet recordSet )
        {
            if ( this.txtWriter == null )
            {
                EnsureDirectoryExists(outputFile.Directory);

                using (TextWriter txtWriter = new StreamWriter(outputFile.FullName, false, Encoding.UTF8))
                using ( CsvWriter writer = new CsvWriter( txtWriter ) )
                {
                    this.writer = writer;
                    WriteHeader( recordSet );
                    WriteRows( recordSet );
                    this.writer = null;
                }
            }
            else
            {
                using ( CsvWriter writer = new CsvWriter( this.txtWriter ) )
                {
                    this.writer = writer;
                    WriteHeader( recordSet );
                    WriteRows( recordSet );
                    this.writer = null;
                }
            }
        }

        private void EnsureDirectoryExists( DirectoryInfo directory )
        {
            if ( !directory.Exists )
            {
                EnsureDirectoryExists( directory.Parent );
                directory.Create();
            }
        }

        private void WriteHeader( IRecordSet recordSet )
        {
            foreach ( IColumn column in recordSet.Columns )
            {
                this.writer.WriteCell( column.Name );
            }
            this.writer.WriteNewRow();
        }

        private void WriteRows( IRecordSet recordSet )
        {
            foreach ( IRecord record in recordSet.Records )
            {
                WriteRow( record );
            }
        }

        private void WriteRow( IRecord record )
        {
            object[] values = record.GetValues();
            for ( int i = 0; i < values.Length; i++ )
            {
                this.writer.WriteCell( values[ i ] );
            }
            this.writer.WriteNewRow();


        }
    }
}