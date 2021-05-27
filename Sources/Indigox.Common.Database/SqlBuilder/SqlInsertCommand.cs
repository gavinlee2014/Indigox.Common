using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Data.SqlBuilder
{
    public class SqlInsertCommand : SqlCommand
    {
        private List<string> fields = new List<string>();
        private List<SqlValue> values = new List<SqlValue>();
        private SqlSelectCommand selectCommand;
        private string tableName;

        public SqlInsertCommand InsertInto( string tableName )
        {
            this.tableName = tableName;
            return this;
        }

        public SqlInsertCommand Fields( params string[] fields )
        {
            this.fields.AddRange( fields );
            return this;
        }

        public SqlInsertCommand Values( params SqlValue[] values )
        {
            this.values.AddRange( values );
            return this;
        }

        /// <summary>
        /// Use Pair( string field, SqlValue value ) instead."
        /// </summary>
        [Obsolete( "Deprecated. Use Pair( string field, SqlValue value ) instead." )]
        public SqlInsertCommand Value( string field, SqlValue value )
        {
            return Pair( field, value );
        }

        public SqlInsertCommand Pair( string field, SqlValue value )
        {
            this.fields.Add( field );
            this.values.Add( value );
            return this;
        }

        public SqlInsertCommand SelectFrom( SqlSelectCommand selectCommand )
        {
            this.selectCommand = selectCommand;
            return this;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append( "insert into " + tableName );

            if ( fields.Count > 0 )
            {
                builder.Append( " (" + string.Join( ", ", fields.ToArray() ) + ")" );
            }

            if ( selectCommand != null )
            {
                builder.Append( " " + selectCommand.ToString() );
            }
            else
            {
                string[] _values = new string[ values.Count ];
                int i = 0;
                foreach ( SqlValue value in values )
                {
                    _values[ i++ ] = value.ToString();
                }
                builder.Append( " values (" + string.Join( ", ", _values ) + ")" );
            }

            return builder.ToString();
        }
    }
}