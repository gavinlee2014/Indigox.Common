
namespace Indigox.TestUtility.Expressions
{
    public class SqlExpression : Expression
    {
        public SqlExpression( string sql )
            : base( null, sql, null, null )
        {

        }

        public override string ToSql()
        {
            return "(" + operate + ")";
        }
    }
}
