using System;
using System.Collections.Generic;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlOrderBy
    {
        private List<string> orders = new List<string>();

        public SqlOrderBy()
        {
        }

        public SqlOrderBy( string[] orders )
        {
            this.orders.AddRange( orders );
        }

        public SqlOrderBy ASC( string field )
        {
            this.orders.Add( field + " asc" );
            return this;
        }

        public SqlOrderBy DESC( string field )
        {
            this.orders.Add( field + " desc" );
            return this;
        }

        public override string ToString()
        {
            return string.Join( ", ", orders.ToArray() );
        }
    }
}