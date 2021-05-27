using Indigox.Common.Data.Interface;
using NUnit.Framework;
using System;

namespace Indigox.Common.Data.Test.DatabaseTests
{
    [TestFixture]
    public class DatabaseTest
    {
        /// <summary>
        ///A test for TestGet
        ///</summary>
        [Test]
        public static void TestQuery()
        {
            DatabaseFactory factory = new DatabaseFactory();
            IDatabase db = factory.CreateDatabase( "BPM" );
            IRecordSet recordSet = db.QueryText( "select * from RuleDefinition where Process='A1F99563-B322-4792-90FA-09CE03C7EA63'" );

        }

        [Test]
        public static void TestErrorQuery()
        {
            Assert.Catch(
                delegate()
                {
                    DatabaseFactory factory = new DatabaseFactory();
                    IDatabase db = factory.CreateDatabase( "BPM" );
                    IRecordSet recordSet = db.QueryText(
                        "select * from RuleDefinition where ",
                        "@p1 varchar, @p2 int, @p3 bigint, @p4 float, @p5 bit, @p6 uniqueidentifier, @p7 datetime",
                        "bob", 100, 100L, 100.1D, true, Guid.NewGuid(), DateTime.Now
                    );
                }
            );
        }
    }
}
