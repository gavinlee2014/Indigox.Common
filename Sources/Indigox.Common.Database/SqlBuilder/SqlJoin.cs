using System;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlJoin
    {
        public static readonly string Join = "";
        public static readonly string CrossJoin = "cross";
        public static readonly string LeftJoin = "left";
        public static readonly string RightJoin = "right";
        public static readonly string FullJoin = "full";

        private string joinType = Join;
        private string tableName;
        private string tableAlias;
        private SqlWhere on;

        public SqlJoin( string joinType, string tableName, SqlWhere on )
        {
            this.joinType = joinType;
            this.tableName = tableName;
            this.on = on;
        }

        public SqlJoin( string joinType, string tableName, string tableAlias, SqlWhere on )
        {
            this.joinType = joinType;
            this.tableName = tableName;
            this.tableAlias = tableAlias;
            this.on = on;
        }

        public override string ToString()
        {
            return ( string.IsNullOrEmpty( joinType ) ? "" : ( joinType + " " ) ) + "join " + tableName + ( string.IsNullOrEmpty( tableAlias ) ? "" : ( " as " + tableAlias ) ) + " on " + on.ToString();
        }
    }
}