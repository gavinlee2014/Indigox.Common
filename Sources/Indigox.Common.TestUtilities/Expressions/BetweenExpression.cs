using Indigox.TestUtility.Builders;

namespace Indigox.TestUtility.Expressions
{
    public class BetweenExpression : Expression
    {
        public BetweenExpression( string name, object value1, object value2 )
            : base( null, "between", name, new object[] { value1, value2 } )
        {

        }

        public override string ToSql()
        {
            return string.Format( "([{0}] {1} {2} and {3})", fieldname, operate, SqlBuilder.GetSqlValueString( values[ 0 ] ), SqlBuilder.GetSqlValueString( values[ 1 ] ) );
        }
    }
}
