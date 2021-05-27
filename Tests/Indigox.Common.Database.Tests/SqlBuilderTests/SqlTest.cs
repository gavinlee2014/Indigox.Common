using System;
using Indigox.Common.Data.SqlBuilder;
using NUnit.Framework;

namespace Indigox.Common.Data.Test.SqlBuilderTests
{
    [TestFixture]
    public class SqlTest
    {
        [Test]
        public void TestSelect()
        {
            string expected = @"select AccountManager from CRM_AccountAccountManager where AccountID = @AccountID";

            string actual = Sql.Select( "AccountManager" )
                     .From( "CRM_AccountAccountManager" )
                     .Where( Sql.Where.Eq( "AccountID", Sql.Param( "AccountID" ) ) )
                     .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestSelectOrderBy()
        {
            string expected = @"select AccountManager from CRM_AccountAccountManager order by name asc, createtime desc";

            string actual = Sql.Select( "AccountManager" )
                    .From( "CRM_AccountAccountManager" )
                    .OrderBy( "name asc", "createtime desc" )
                    .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestSelectAllData()
        {
            string expected = @"select newid() as id from CRM_AccountAccountManager as t join t0 on t.ID = t0.Child";

            string actual = Sql.Select( "newid() as id" )
                    .From( "CRM_AccountAccountManager", "t" )
                    .Join( "t0", Sql.Where.Eq( "t.ID", "t0.Child" ) )
                    .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestSelectPagedData()
        {
            int start = 0;
            int limit = 10;

            string expected = @"select AccountManager from (" +
                    @"select AccountManager, row_number() over(order by current_timestamp) as _RowNum " +
                    @"from CRM_AccountAccountManager where AccountID = @AccountID" +
                @") u where u._RowNum between 1 and 10";

            string actual = Sql.Select( "AccountManager" )
                    .From( "CRM_AccountAccountManager" )
                    .Where( Sql.Where.Eq( "AccountID", Sql.Param( "AccountID" ) ) )
                    .StartFrom( start )
                    .Limit( limit )
                    .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestSelectPagedDataOrderBy()
        {
            int start = 0;
            int limit = 10;

            string expected = @"select AccountManager from (" +
                    @"select AccountManager, row_number() over(order by name asc, createtime desc) as _RowNum " +
                    @"from CRM_AccountAccountManager" +
                @") u where u._RowNum between 1 and 10";

            string actual = Sql.Select( "AccountManager" )
                    .From( "CRM_AccountAccountManager" )
                    .OrderBy( "name asc", "createtime desc" )
                    .StartFrom( start )
                    .Limit( limit )
                    .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestSelectJoinMultiTables()
        {
            int start = 0;
            int limit = 10;

            string expected = @"select AccountManagerID from (" +
                    @"select t2.ID as AccountManagerID, row_number() over(order by current_timestamp) as _RowNum " +
                    @"from Membership as t0 join Principal as t1 on (t0.Parent = t1.ID and t1.Type = 300) " +
                    @"left join Principal as t2 on (t0.Child = t2.ID and t2.Type between 200 and 299) " +
                    @"where t1.ID = @Organization" +
                @") u where u._RowNum between 1 and 10";

            string actual = Sql.Select( "t2.ID as AccountManagerID" )
                .From( "Membership", "t0" )
                .Join( "Principal", "t1", Sql.And(
                                            Sql.Where.Eq( "t0.Parent", "t1.ID" ),
                                            Sql.Where.Eq( "t1.Type", Sql.Value( 300 ) )
                                          ) )
                .LeftJoin( "Principal", "t2", Sql.And(
                                            Sql.Where.Eq( "t0.Child", "t2.ID" ),
                                            Sql.Where.Between( "t2.Type", Sql.Value( 200 ), Sql.Value( 299 ) )
                                          ) )
                .Where( Sql.Where.Eq( "t1.ID", Sql.Param( "Organization" ) ) )
                .StartFrom( start )
                .Limit( limit )
                .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestDelete()
        {
            string expected = @"delete from CRM_AccountAccountManager where (AccountID = @AccountID and AccountManager = @AccountManager)";

            string actual = Sql.Delete()
                     .From( "CRM_AccountAccountManager" )
                     .Where( Sql.And(
                             Sql.Where.Eq( "AccountID", Sql.Param( "AccountID" ) ),
                             Sql.Where.Eq( "AccountManager", Sql.Param( "AccountManager" ) )
                         ) )
                     .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestInsertValues()
        {
            string expected = @"insert into CRM_AccountAccountManager (AccountID, AccountManager) values (@AccountID, @AccountManager)";

            string actual = Sql.Insert( "CRM_AccountAccountManager" )
                    .Fields( "AccountID", "AccountManager" )
                    .Values( Sql.Param( "AccountID" ), Sql.Param( "AccountManager" ) )
                    .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestInsertPairs()
        {
            string expected = @"insert into CRM_AccountAccountManager (AccountID, AccountManager) values (@AccountID, @AccountManager)";

            string actual = Sql.Insert( "CRM_AccountAccountManager" )
                    .Pair( "AccountID", Sql.Param( "AccountID" ) )
                    .Pair( "AccountManager", Sql.Param( "AccountManager" ) )
                    .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestInsertSelectFrom()
        {
            string expected = @"insert into CRM_AccountAccountManager (AccountID, AccountManager) " +
                    @"select AccountID, AccountManager from TempTable as t where AccountID is not null";

            string actual = Sql.Insert( "CRM_AccountAccountManager" )
                    .Fields( "AccountID", "AccountManager" )
                    .SelectFrom(
                            Sql.Select( "AccountID", "AccountManager" )
                                .From( "TempTable", "t" )
                                .Where( Sql.Where.IsNotNull( "AccountID" ) )
                        )
                    .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestUpdate()
        {
            string expected = @"update CRM_AccountAccountManager set AccountID = @AccountID, AccountManager = @AccountManager " +
                    @"where AccountID is not null";

            string actual = Sql.Update( "CRM_AccountAccountManager" )
                    .Set( "AccountID", Sql.Param( "AccountID" ) )
                    .Set( "AccountManager", Sql.Param( "AccountManager" ) )
                    .Where( Sql.Where.IsNotNull( "AccountID" ) )
                    .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestUpdateFromJoinTables()
        {
            string expected = @"update t set AccountID = t0.AccountID, AccountManager = t0.AccountManager " +
                    @"from CRM_AccountAccountManager as t join t0 on t.ID = t0.Child";

            string actual = Sql.Update( "t" )
                    .Set( "AccountID", Sql.Expression( "t0.AccountID" ) )
                    .Set( "AccountManager", Sql.Expression( "t0.AccountManager" ) )
                    .From( "CRM_AccountAccountManager", "t" )
                    .Join( "t0", Sql.Where.Eq( "t.ID", "t0.Child" ) )
                    .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestNonRecrusiveCteSelect()
        {
            string expected = @"with cte (id, pid, name, fullname) as (" +
                    @"select t.id, t.organization, t.name, convert(nvarchar(4000), rtrim(t.name)) as fullname " +
                    @"from principal as t where t.organization is null" +
                @") select * from cte";

            string actual = Sql.Cte( "cte" )
                .Fields( "id", "pid", "name", "fullname" )
                .SelectFrom( Sql.Select( "t.id", "t.organization", "t.name", "convert(nvarchar(4000), rtrim(t.name)) as fullname" )
                     .From( "principal", "t" )
                     .Where( Sql.Where.IsNull( "t.organization" ) ) )
                .Do( Sql.Select( "*" )
                     .From( "cte" ) )
                .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestRecrusiveCteSelect()
        {
            string expected = @"with cte (id, pid, name, fullname) as (" +
                    @"select t.id, t.organization, t.name, convert(nvarchar(4000), rtrim(t.name)) as fullname " +
                    @"from principal as t where t.organization is null " +
                    @"union all " +
                    @"select t.id, t.organization, t.name, convert(nvarchar(4000), cte.fullname+'_'+rtrim(t.name)) as fullname " +
                    @"from principal as t join cte on t.organization = cte.id" +
                @") select * from cte";

            string actual = Sql.Cte( "cte" )
                .Fields( "id", "pid", "name", "fullname" )
                .SelectFrom( Sql.Select( "t.id", "t.organization", "t.name", "convert(nvarchar(4000), rtrim(t.name)) as fullname" )
                    .From( "principal", "t" )
                    .Where( Sql.Where.IsNull( "t.organization" ) ) )
                .UnionAll( Sql.Select( "t.id", "t.organization", "t.name", "convert(nvarchar(4000), cte.fullname+'_'+rtrim(t.name)) as fullname" )
                    .From( "principal", "t" )
                    .Join( "cte", Sql.Where.Eq( "t.organization", "cte.id" ) ) )
                .Do( Sql.Select( "*" )
                    .From( "cte" ) )
                .ToString();

            Console.WriteLine( actual );

            Assert.AreEqual( expected, actual );
        }
    }
}