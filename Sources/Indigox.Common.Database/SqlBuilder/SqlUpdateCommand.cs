using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlUpdateCommand : SqlCommand
    {
        private string updateTableName;
        private List<SqlJoin> joins = new List<SqlJoin>();
        private string tableName;
        private string tableAlias;
        private SqlWhere where;
        private List<SqlUpdateSetStatement> updateSetStatements = new List<SqlUpdateSetStatement>();

        public SqlUpdateCommand Update( string tableName )
        {
            this.updateTableName = tableName;
            return this;
        }

        public SqlUpdateCommand Set( string fieldName, SqlValue value )
        {
            this.updateSetStatements.Add( new SqlUpdateSetStatement( fieldName, value ) );
            return this;
        }

        public SqlUpdateCommand Set( string fieldName, string expression )
        {
            this.updateSetStatements.Add( new SqlUpdateSetStatement( fieldName, expression ) );
            return this;
        }

        public SqlUpdateCommand From( string tableName )
        {
            this.tableName = tableName;
            return this;
        }

        public SqlUpdateCommand From( string tableName, string tableAlias )
        {
            this.tableName = tableName;
            this.tableAlias = tableAlias;
            return this;
        }

        public SqlUpdateCommand Join( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.Join, tableName, on ) );
            return this;
        }

        public SqlUpdateCommand Join( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.Join, tableName, tableAlias, on ) );
            return this;
        }

        public SqlUpdateCommand CrossJoin( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.CrossJoin, tableName, on ) );
            return this;
        }

        public SqlUpdateCommand CrossJoin( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.CrossJoin, tableName, tableAlias, on ) );
            return this;
        }

        public SqlUpdateCommand LeftJoin( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.LeftJoin, tableName, on ) );
            return this;
        }

        public SqlUpdateCommand LeftJoin( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.LeftJoin, tableName, tableAlias, on ) );
            return this;
        }

        public SqlUpdateCommand RightJoin( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.RightJoin, tableName, on ) );
            return this;
        }

        public SqlUpdateCommand RightJoin( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.RightJoin, tableName, tableAlias, on ) );
            return this;
        }

        public SqlUpdateCommand FullJoin( string tableName, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.FullJoin, tableName, on ) );
            return this;
        }

        public SqlUpdateCommand FullJoin( string tableName, string tableAlias, SqlWhere on )
        {
            joins.Add( new SqlJoin( SqlJoin.FullJoin, tableName, tableAlias, on ) );
            return this;
        }

        public SqlUpdateCommand Where( SqlWhere where )
        {
            this.where = where;
            return this;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            string[] _updateSetStatements = new string[ updateSetStatements.Count ];
            int i = 0;
            foreach ( SqlUpdateSetStatement updateSetStatement in updateSetStatements )
            {
                _updateSetStatements[ i++ ] = updateSetStatement.ToString();
            }

            builder.Append( "update " + updateTableName );
            builder.Append( " set " + string.Join( ", ", _updateSetStatements ) );

            if ( !string.IsNullOrEmpty( tableName ) )
            {
                builder.Append( " from " + tableName + ( string.IsNullOrEmpty( tableAlias ) ? "" : ( " as " + tableAlias ) ) );
                foreach ( SqlJoin join in joins )
                {
                    builder.Append( " " + join.ToString() );
                }
            }
            else
            {
                builder.Append( " where " + where.ToString() );
            }

            return builder.ToString();
        }
    }
}