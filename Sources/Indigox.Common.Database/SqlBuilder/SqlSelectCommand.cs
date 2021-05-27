using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Logging;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlSelectCommand : SqlCommand
    {
        private List<string> fields = new List<string>();
        private List<string> fieldAlias = new List<string>();
        private List<SqlJoin> joins = new List<SqlJoin>();
        private string tableName;
        private string tableAlias;
        private SqlWhere whereClause;
        private int startFrom;
        private int limit;
        private SqlOrderBy orderBy;
        private string rowNumField = "_RowNum";

        public SqlWhere WhereClause
        {
            get { return whereClause; }
            set { whereClause = value; }
        }

        public SqlSelectCommand Select( params string[] fields )
        {
            foreach ( string field in fields )
            {
                this.fields.Add( field );
                this.fieldAlias.Add( GetAlias( field ) );
            }
            return this;
        }

        public SqlSelectCommand From( string tableName )
        {
            this.tableName = tableName;
            return this;
        }

        public SqlSelectCommand From( string tableName, string tableAlias )
        {
            this.tableName = tableName;
            this.tableAlias = tableAlias;
            return this;
        }

        public SqlSelectCommand Join( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.Join, tableName, on ) );
            return this;
        }

        public SqlSelectCommand Join( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.Join, tableName, tableAlias, on ) );
            return this;
        }

        public SqlSelectCommand CrossJoin( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.CrossJoin, tableName, on ) );
            return this;
        }

        public SqlSelectCommand CrossJoin( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.CrossJoin, tableName, tableAlias, on ) );
            return this;
        }

        public SqlSelectCommand LeftJoin( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.LeftJoin, tableName, on ) );
            return this;
        }

        public SqlSelectCommand LeftJoin( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.LeftJoin, tableName, tableAlias, on ) );
            return this;
        }

        public SqlSelectCommand RightJoin( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.RightJoin, tableName, on ) );
            return this;
        }

        public SqlSelectCommand RightJoin( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.RightJoin, tableName, tableAlias, on ) );
            return this;
        }

        public SqlSelectCommand FullJoin( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.FullJoin, tableName, on ) );
            return this;
        }

        public SqlSelectCommand FullJoin( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.FullJoin, tableName, tableAlias, on ) );
            return this;
        }

        public SqlSelectCommand Where( SqlWhere where )
        {
            this.whereClause = where;
            return this;
        }

        public SqlSelectCommand StartFrom( int startFrom )
        {
            this.startFrom = startFrom;
            return this;
        }

        public SqlSelectCommand Limit( int limit )
        {
            this.limit = limit;
            return this;
        }

        public SqlSelectCommand OrderBy( params string[] orders )
        {
            this.orderBy = new SqlOrderBy( orders );
            return this;
        }

        public SqlSelectCommand OrderBy( SqlOrderBy orderBy )
        {
            this.orderBy = orderBy;
            return this;
        }

        private string GetAlias( string fieldName )
        {
            string[] tokens = SqlParser.ParseTokens( fieldName );
            Log.Debug( "tokens: " + string.Join( " ", tokens ) );

            string token = tokens[ tokens.Length - 1 ];
            if ( tokens.Length == 1 )
            {
                int index = token.IndexOf( "." );
                if ( index > 0 )
                {
                    return token.Substring( index + 1 );
                }
                else
                {
                    return token;
                }
            }

            return token;
        }

        public override string ToString()
        {
            bool enablePaging = ( limit > 0 );

            StringBuilder builder = new StringBuilder();
            if ( enablePaging )
            {
                builder.Append( "select " + string.Join( ", ", fieldAlias.ToArray() ) );
                builder.Append( " from (" );
            }
            builder.Append( "select " + string.Join( ", ", fields.ToArray() ) );
            if ( enablePaging )
            {
                builder.Append( ", row_number() over(order by " + ( ( orderBy == null ) ? "current_timestamp" : orderBy.ToString() ) + ") as " + rowNumField );
            }
            builder.Append( " from " + tableName + ( string.IsNullOrEmpty( tableAlias ) ? "" : ( " as " + tableAlias ) ) );
            foreach ( SqlJoin join in joins )
            {
                builder.Append( " " + join.ToString() );
            }
            if ( whereClause != null )
            {
                builder.Append( " where " + whereClause.ToString() );
            }
            if ( enablePaging )
            {
                builder.Append( ") u where u." + rowNumField + " between " + ( startFrom + 1 ) + " and " + ( startFrom + limit ) );
            }
            else
            {
                if ( orderBy != null )
                {
                    builder.Append( " order by " + orderBy.ToString() );
                }
            }

            return builder.ToString();
        }
    }
}