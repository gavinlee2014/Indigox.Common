using System;

namespace Indigox.Common.Data.SqlBuilder
{
    internal class SqlUpdateSetStatement
    {
        private string fieldName;
        private string expression;

        public SqlUpdateSetStatement( string fieldName, SqlValue value )
        {
            this.fieldName = fieldName;
            this.expression = value.ToString();
        }

        public SqlUpdateSetStatement( string fieldName, string expression )
        {
            this.fieldName = fieldName;
            this.expression = expression;
        }

        public override string ToString()
        {
            return fieldName + " = " + expression;
        }
    }
}