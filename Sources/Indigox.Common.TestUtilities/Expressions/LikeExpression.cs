using Indigox.TestUtility.Builders;

namespace Indigox.TestUtility.Expressions
{
    public class LikeExpression : Expression
    {
        public LikeExpression( string name, object value )
            : base( null, "like", name, new object[] { value } )
        {

        }

        public override string ToSql()
        {
            return string.Format( "([{0}] {1} {2})", fieldname, operate, SqlBuilder.GetSqlValueString( values[ 0 ] ) );
        }
    }
}
