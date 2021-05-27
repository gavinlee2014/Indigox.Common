using System;
using System.IO;
using Indigox.Common.Data;
using Indigox.Common.Data.Interface;
using Indigox.Common.Statistics.Exporters;
using NUnit.Framework;

namespace Indigox.Common.Statistics.NUnitTest.ExporterTests
{
    [TestFixture]
    public class CsvExporterTest
    {
        [Test]
        public void TestExport()
        {
            IRecordSet dataSet = new DatabaseFactory().CreateDatabase( "BPM" ).QueryText( @"
                    select name [表名] 
                    from sys.tables
            " );
            CsvExporter exporter = new CsvExporter( new FileInfo( "output.csv" ) );
            exporter.Export( dataSet );
        }
    }
}