
namespace Indigox.TestUtility.Expressions
{
    public class IsNullExpression : Expression
    {
        public IsNullExpression( string name )
            : base( null, "isnull", name, null )
        {

        }

        public override string ToSql()
        {
            return "(" + fieldname + " is null)";
        }
    }
}
