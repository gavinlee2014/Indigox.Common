using System;
using Indigox.Common.Logging;
using Indigox.TestUtility.Expressions;

namespace Indigox.TestUtility.Builders
{
    class SqlBuilder
    {

        public static string GenerateGetValueSql( string table, string field, Expression expression )
        {
            string sql = ( "select top 1 " + field + " as [value] from [" + table + "]" );
            if ( expression != null )
            {
                string where = " where " + expression.ToSql();
                //Log.Debug( where );
                sql += where;
            }
            return sql;
        }

        public static string GenerateCountSql( string table, Expression expression )
        {
            string sql = ( "select count(1) from [" + table + "]" );
            if ( expression != null )
            {
                string where = " where " + expression.ToSql();
                Log.Debug( where );
                sql += where;
            }
            return sql;
        }

        public static string GenerateDeleteSql( string table, Expression expression )
        {
            string sql = ( "delete from [" + table + "]" );
            if ( expression != null )
            {
                string where = " where " + expression.ToSql();
                //Log.Debug( where );
                sql += where;
            }
            return sql;
        }

        public static string GetSqlValueString( object value )
        {
            if ( value == null )
                return "null";
            if ( PrimativeTypes.IsString( value ) )
                return ( "'" + ( (string)value ).Replace( "'", "''" ) + "'" );
            if ( PrimativeTypes.IsBoolean( value ) )
                return ( (bool)value ) ? "1" : "0";
            if ( PrimativeTypes.IsNumber( value ) )
                return value.ToString();
            if ( PrimativeTypes.IsDateTime( value ) )
                return ( "'" + ( (DateTime)value ).ToString( "yyyy-MM-dd HH:mm:ss" ) + "'" );
            return "'" + value.ToString() + "'";
        }
    }
}
