using System;
using System.Text.RegularExpressions;
using Indigox.Common.Data;
using Indigox.Common.Data.Interface;
using Indigox.Common.Logging;
using Indigox.TestUtility.Builders;
using Indigox.TestUtility.Expressions;

namespace Indigox.TestUtility
{
    public class DbUtil
    {
        private static DatabaseFactory factory = new DatabaseFactory();

        private IDatabase db = null;

        private DbUtil()
        {
        }

        public static DbUtil Get( string dbname )
        {
            DbUtil instance = new DbUtil();
            instance.db = factory.CreateDatabase( dbname );
            return instance;
        }

        public void Insert( string sql )
        {
            db.ExecuteText( "/*dbutil*/ " + sql );
        }

        public int InsertAndReturnIdentity( string sql )
        {
            return (int)db.ScalarText( "/*dbutil*/ " + sql + "; SELECT @@identity;" );
        }

        public void IdentityInsert( string table, string sql )
        {
            db.ExecuteText( "/*dbutil*/ SET IDENTITY_INSERT [" + table + "] ON; " + sql );
        }

        public void ClearData( string sql )
        {
            sql = CompressSQL( sql );
            db.ExecuteText( "/*dbutil*/ " + sql );
        }

        private string CompressSQL( string sql )
        {
            sql = compressSQLRegex.Replace( sql, "$1 " );
            return sql;
        }

        static Regex compressSQLRegex = new Regex( @"(?:('[^']*')\s*)|(\s+)" );

        public void ClearData( string table, Expression expression )
        {
            string sql = SqlBuilder.GenerateDeleteSql( table, expression );
            ClearData( sql );
        }

        public void TruncateTable( string table )
        {
            db.ExecuteText( "/*dbutil*/ TRUNCATE TABLE [" + table + "]" );
        }

        public int GetCount( string sql )
        {
            return (int)db.ScalarText( "/*dbutil*/ " + sql );
        }

        public int GetCount( string table, Expression expression )
        {
            string sql = SqlBuilder.GenerateCountSql( table, expression );
            return GetCount( sql );
        }

        public int? GetInt( string sql )
        {
            object val = db.ScalarText( "/*dbutil*/ " + sql );
            int? ret = (int?)val;
            return ret;
        }

        public int? GetInt( string table, string field, Expression expression )
        {
            string sql = SqlBuilder.GenerateGetValueSql( table, field, expression );
            return GetInt( sql );
        }

        public Guid? GetGuid( string sql )
        {
            object val = db.ScalarText( "/*dbutil*/ " + sql );
            Guid? ret = (Guid?)val;
            return ret;
        }

        public Guid? GetGuid( string table, string field, Expression expression )
        {
            string sql = SqlBuilder.GenerateGetValueSql( table, field, expression );
            return GetGuid( sql );
        }

        public string GetString( string sql )
        {
            object val = db.ScalarText( "/*dbutil*/ " + sql );
            string ret = (string)val;
            return ret;
        }

        public string GetString( string table, string field, Expression expression )
        {
            string sql = SqlBuilder.GenerateGetValueSql( table, field, expression );
            return GetString( sql );
        }

        public object GetValue( string sql )
        {
            object val = db.ScalarText( "/*dbutil*/ " + sql );
            return val;
        }

        public object GetValue( string table, string field, Expression expression )
        {
            string sql = SqlBuilder.GenerateGetValueSql( table, field, expression );
            return GetValue( sql );
        }
    }
}
