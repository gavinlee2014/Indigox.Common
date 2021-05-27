using System;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlDeleteCommand : SqlCommand
    {
        private string tableName;
        private string tableAlias;
        private SqlWhere where;

        public SqlDeleteCommand From( string tableName )
        {
            this.tableName = tableName;
            return this;
        }

        public SqlDeleteCommand From( string tableName, string tableAlias )
        {
            this.tableName = tableName;
            this.tableAlias = tableAlias;
            return this;
        }

        public SqlDeleteCommand Where( SqlWhere where )
        {
            this.where = where;
            return this;
        }

        public override string ToString()
        {
            return "delete from " + tableName + ( string.IsNullOrEmpty( tableAlias ) ? "" : ( " as " + tableAlias ) ) + " where " + where.ToString();
        }
    }
}