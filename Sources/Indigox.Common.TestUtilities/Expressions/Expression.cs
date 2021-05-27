using System.Collections.Generic;

namespace Indigox.TestUtility.Expressions
{
    public abstract class Expression
    {
        protected Expression( string conjuction, string operate, string fieldname, object[] values )
        {
            this.children = new List<Expression>();
            this.conjuction = conjuction;
            this.operate = operate;
            this.fieldname = fieldname;
            this.values = values;
        }

        public abstract string ToSql();

        public override string ToString()
        {
            return ToSql();
        }

        protected List<Expression> children;
        protected string conjuction;
        protected string operate;
        protected string fieldname;
        protected object[] values;
    }
}
