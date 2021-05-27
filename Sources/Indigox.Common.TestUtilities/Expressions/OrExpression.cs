
namespace Indigox.TestUtility.Expressions
{
    public class OrExpression : Expression
    {
        public OrExpression( params Expression[] expressions )
            : base( "or", null, null, null )
        {
            this.children.AddRange( expressions );
        }

        public override string ToSql()
        {
            string[] sqls = new string[ children.Count ];
            for ( int i = 0 ; i < sqls.Length ; i++ )
            {
                sqls[ i ] = children[ i ].ToSql();
            }
            return "(" + string.Join( " or ", sqls ) + ")";
        }
    }
}
