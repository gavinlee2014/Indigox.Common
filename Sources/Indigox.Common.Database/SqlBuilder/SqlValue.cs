using System;
using Indigox.Common.Data.Utils;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlValue
    {
        private object value;

        protected SqlValue()
        {
        }

        public SqlValue( object value )
        {
            this.value = value;
        }

        public override string ToString()
        {
            return SqlValueConvert.ToSqlString( value );
        }
    }
}