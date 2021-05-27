using System;
using System.Diagnostics;
using Indigox.Common.Data.Configuration;
using Indigox.Common.Data.Interface;
using NUnit.Framework;

namespace Indigox.Common.Data.Test.DatabaseTests
{
    [TestFixture]
    public class RecordSetPerformanceTest
    {
        [Test]
        public void TestQueryRecordSet()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            DatabaseFactory factory = new DatabaseFactory();
            IDatabase db = factory.CreateDatabase( connectionName );
            IRecordSet recordSet = db.QueryText( sql );

            Console.WriteLine( "{0} ms", sw.ElapsedMilliseconds );
        }

        [Test]
        public void TestQueryDataSet()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection( DatabaseSection.Default.Connections[ connectionName ].ConnectionString );
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand( sql, conn );
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter( cmd );
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill( ds );

            Console.WriteLine( "{0} ms", sw.ElapsedMilliseconds );
        }

        [Test]
        public void TestTraverseRecordSet()
        {
            DatabaseFactory factory = new DatabaseFactory();
            IDatabase db = factory.CreateDatabase( connectionName );
            IRecordSet recordSet = db.QueryText( sql );

            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach ( IRecord record in recordSet.Records )
            {
                foreach ( IColumn column in record.Columns )
                {
                    object value = record.GetValue( column.Name );
                }
            }
            Console.WriteLine( "{0} ms", sw.ElapsedMilliseconds );
        }

        [Test]
        public void TestTraverseDataSet()
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection( DatabaseSection.Default.Connections[ connectionName ].ConnectionString );
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand( sql, conn );
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter( cmd );
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill( ds );

            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach ( System.Data.DataRow row in ds.Tables[ 0 ].Rows )
            {
                foreach ( System.Data.DataColumn col in row.Table.Columns )
                {
                    object val =  row[ col ];
                }
            }
            Console.WriteLine( "{0} ms", sw.ElapsedMilliseconds );
        }

        string sql = "select * from processdefinition";
        string connectionName = "BPM";
    }
}
