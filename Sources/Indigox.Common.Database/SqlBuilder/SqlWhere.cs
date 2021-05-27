using System;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlWhere
    {
        private string clause;

        public SqlWhere()
        {
            clause = string.Empty;
        }

        public SqlWhere( string clause )
        {
            this.clause = clause;
        }

        public SqlWhere Eq( string fieldName, SqlValue value )
        {
            //TODO:
            clause = fieldName + " = " + value.ToString();
            return this;
        }

        public SqlWhere Eq( string leftFieldName, string rightFieldName )
        {
            //TODO:
            clause = leftFieldName + " = " + rightFieldName;
            return this;
        }

        public SqlWhere Like( string fieldName, SqlValue value )
        {
            //TODO:
            clause = fieldName + " like " + value.ToString();
            return this;
        }

        public SqlWhere Like( string leftFieldName, string rightFieldName )
        {
            //TODO:
            clause = leftFieldName + " like " + rightFieldName;
            return this;
        }

        public SqlWhere Gt( string fieldName, SqlValue value )
        {
            //TODO:
            clause = fieldName + " > " + value.ToString();
            return this;
        }

        public SqlWhere Gt( string leftFieldName, string rightFieldName )
        {
            //TODO:
            clause = leftFieldName + " > " + rightFieldName;
            return this;
        }

        public SqlWhere Gte( string fieldName, SqlValue value )
        {
            //TODO:
            clause = fieldName + " >= " + value.ToString();
            return this;
        }

        public SqlWhere Gte( string leftFieldName, string rightFieldName )
        {
            //TODO:
            clause = leftFieldName + " >= " + rightFieldName;
            return this;
        }

        public SqlWhere Lt( string fieldName, SqlValue value )
        {
            //TODO:
            clause = fieldName + " < " + value.ToString();
            return this;
        }

        public SqlWhere Lt( string leftFieldName, string rightFieldName )
        {
            //TODO:
            clause = leftFieldName + " < " + rightFieldName;
            return this;
        }

        public SqlWhere Lte( string fieldName, SqlValue value )
        {
            //TODO:
            clause = fieldName + " <= " + value.ToString();
            return this;
        }

        public SqlWhere Lte( string leftFieldName, string rightFieldName )
        {
            //TODO:
            clause = leftFieldName + " <= " + rightFieldName;
            return this;
        }

        public SqlWhere Between( string fieldName, SqlValue minValue, SqlValue maxValue )
        {
            //TODO:
            clause = fieldName + " between " + minValue.ToString() + " and " + maxValue.ToString();
            return this;
        }

        public SqlWhere IsNull( string fieldName )
        {
            //TODO:
            clause = fieldName + " is null";
            return this;
        }

        public SqlWhere IsNotNull( string fieldName )
        {
            //TODO:
            clause = fieldName + " is not null";
            return this;
        }

        public override string ToString()
        {
            return clause;
        }
    }
}