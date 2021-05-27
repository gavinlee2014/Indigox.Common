using System;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlParam : SqlValue
    {
        private string paramName;

        public SqlParam( string paramName )
        {
            this.paramName = paramName;
        }

        public override string ToString()
        {
            return "@" + paramName;
        }
    }
}