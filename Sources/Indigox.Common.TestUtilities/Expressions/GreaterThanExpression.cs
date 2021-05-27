﻿using Indigox.TestUtility.Builders;

namespace Indigox.TestUtility.Expressions
{
    public class GreaterThanExpression : Expression
    {
        public GreaterThanExpression( string name, object value )
            : base( null, ">", name, new object[] { value } )
        {

        }

        public override string ToSql()
        {
            return string.Format( "([{0}] {1} {2})", fieldname, operate, SqlBuilder.GetSqlValueString( values[ 0 ] ) );
        }
    }
}
