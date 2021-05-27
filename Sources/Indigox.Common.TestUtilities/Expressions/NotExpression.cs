
namespace Indigox.TestUtility.Expressions
{
    public class NotExpression : Expression
    {
        public NotExpression( Expression expression )
            : base( "not", null, null, null )
        {
            this.children.Add( expression );
        }

        public override string ToSql()
        {
            return string.Format( "(not ({0}))", children[ 0 ].ToSql() );
        }
    }
}
