using Indigox.TestUtility.Builders;
using Indigox.TestUtility.Expressions;
using NUnit.Framework;

namespace Indigox.TestUtility
{
    public class DbAssert
    {
        private static readonly string NullString = null;

        private DbUtil dbUtil = null;

        private DbAssert()
        {
        }

        public static DbAssert Get( string dbname )
        {
            DbAssert instance = new DbAssert();
            instance.dbUtil = DbUtil.Get( dbname );
            return instance;
        }

        public void AreCountEquals( int expected, string sql )
        {
            AreCountEquals( expected, sql, NullString );
        }

        public void AreCountEquals( int expected, string sql, string message )
        {
            int? count = dbUtil.GetInt( sql );
            Assert.AreEqual( expected, count, message );
        }

        public void AreCountEquals( int expected, string table, Expression expression )
        {
            AreCountEquals( expected, table, expression, NullString );
        }

        public void AreCountEquals( int expected, string table, Expression expression, string message )
        {
            string sql = SqlBuilder.GenerateCountSql( table, expression );
            AreCountEquals( expected, sql, message );
        }

        public void AreExists( string sql )
        {
            AreExists( sql, "Expected exsits in database, but not exists." );
        }

        public void AreExists( string sql, string message )
        {
            int? count = dbUtil.GetInt( sql );
            if ( count == 0 )
            {
                Assert.Fail( message );
            }
        }

        public void AreExists( string table, Expression expression )
        {
            AreExists( table, expression, "Expected exsits in database, but not exists." );
        }

        public void AreExists( string table, Expression expression, string message )
        {
            string sql = SqlBuilder.GenerateCountSql( table, expression );
            AreExists( sql, message );
        }

        public void AreNotExists( string sql )
        {
            AreNotExists( sql, "Expected not exsits in database, but exists." );
        }

        public void AreNotExists( string sql, string message )
        {
            int? count = dbUtil.GetInt( sql );
            if ( count != 0 )
            {
                Assert.Fail( message );
            }
        }

        public void AreNotExists( string table, Expression expression )
        {
            AreNotExists( table, expression, "Expected not exsits in database, but exists." );
        }

        public void AreNotExists( string table, Expression expression, string message )
        {
            string sql = SqlBuilder.GenerateCountSql( table, expression );
            AreNotExists( sql, message );
        }

        public void AreFieldEquals( object expected, string table, string fieldname, Expression expression )
        {
            AreFieldEquals( expected, table, fieldname, expression, "filed [" + fieldname + "] value doesn't match." );
        }

        public void AreFieldEquals( object expected, string table, string fieldname, Expression expression, string message )
        {
            object val = dbUtil.GetValue( table, fieldname, expression );
            Assert.AreEqual( expected, val, message );
        }
    }
}
