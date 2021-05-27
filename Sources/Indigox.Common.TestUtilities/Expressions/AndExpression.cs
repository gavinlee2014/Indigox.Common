using System;

namespace Indigox.TestUtility.Expressions
{
    public class AndExpression : Expression
    {
        public AndExpression( params Expression[] expressions )
            : base( "and", null, null, null )
        {
            if ( expressions.Length < 2 )
                throw new ArgumentException( "AndExpression required 2 arguments at least." );
            this.children.AddRange( expressions );
        }

        public override string ToSql()
        {
            string[] sqls = new string[ children.Count ];
            for ( int i = 0 ; i < sqls.Length ; i++ )
            {
                sqls[ i ] = children[ i ].ToSql();
            }
            return "(" + string.Join( " and ", sqls ) + ")";
        }
    }
}
