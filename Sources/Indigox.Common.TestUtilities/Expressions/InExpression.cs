using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.TestUtility.Expressions
{
    public class InExpression : Expression
    {
        private string column;
        private string subSelectTable;
        private string keyColumn;
        private Expression subSelectWhereClause;

        public InExpression( string column, string keyColumn, string subSelectTable, Expression subSelectWhereClause )
            : base( null, "in", column, new object[] { subSelectTable, keyColumn, subSelectWhereClause } )
        {
            this.column = column;
            this.subSelectTable = subSelectTable;
            this.keyColumn = keyColumn;
            this.subSelectWhereClause = subSelectWhereClause;
        }

        public override string ToSql()
        {
            return string.Format( "([{0}] in (select [{2}] from [{1}] where {3}))",
                                  this.column,
                                  this.subSelectTable,
                                  this.keyColumn,
                                  this.subSelectWhereClause.ToSql() );
        }
    }
}
