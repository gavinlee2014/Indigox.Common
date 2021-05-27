using System;
using System.Threading;
using Indigox.Common.Data.Interface;
using NUnit.Framework;

namespace Indigox.Common.Data.Test.DatabaseTests
{
    [TestFixture]
    class QueryWithMultiThreadTest
    {
        [Test]
        [TestCase( 200 )]
        public void TestQueryWithMultiThread( int times )
        {
            ThreadStart[] testMethods = new ThreadStart[] { Test1, Test2 };
            for ( int i = 0 ; i < times ; i++ )
            {
                Thread thread = new Thread( testMethods[ i % testMethods.Length ] );
                thread.Start();
            }
            while ( completed < times )
            {
                Thread.Sleep( 1000 );
            }
        }

        int completed = 0;

        void Test1()
        {
            DatabaseFactory factory = new DatabaseFactory();
            IDatabase db = factory.CreateDatabase( "BPM" );
            IRecordSet recordSet = db.QueryText( "SELECT top 100 [GUID],[Sign],[Type],[DescriptorType] FROM [Descriptor] with (nolock)" );
            foreach ( IRecord record in recordSet.Records )
            {
                Console.WriteLine( "{0,-4} {1,-20} {2}", Thread.CurrentThread.ManagedThreadId, "DescriptorType", record.GetValue( "DescriptorType" ) );
            }
            Interlocked.Increment( ref completed );
        }

        void Test2()
        {
            DatabaseFactory factory = new DatabaseFactory();
            IDatabase db = factory.CreateDatabase( "BPM" );
            IRecordSet recordSet = db.QueryText( "SELECT top 100 [GUID],[InstanceType],[Parent],[ParentType],[Root],[RootType] FROM [Instance] with (nolock)" );
            foreach ( IRecord record in recordSet.Records )
            {
                Console.WriteLine( "{0,-4} {1,-20} {2}", Thread.CurrentThread.ManagedThreadId, "Root", record.GetValue( "Root" ) );
            }
            Interlocked.Increment( ref completed );
        }
    }
}
