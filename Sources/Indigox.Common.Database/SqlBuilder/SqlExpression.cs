using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlExpression : SqlValue
    {
        private string exp;

        public SqlExpression( string exp )
        {
            this.exp = exp;
        }

        public override string ToString()
        {
            return exp;
        }
    }
}
