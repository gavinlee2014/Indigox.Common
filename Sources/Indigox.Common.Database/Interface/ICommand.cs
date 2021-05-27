using System;
using System.Collections.Generic;

namespace Indigox.Common.Data.Interface
{
    public interface ICommand
    {
        /// <summary>
        /// Gets the Parameters.
        /// </summary>
        IList<IParameter> Parameters { get; }
        /// <summary>
        /// Gets or sets a value indicating how the System.Data.SqlClient.SqlCommand.CommandText property is to be interpreted.
        /// </summary>
        CommandType CommandType { get; set; }
        /// <summary>
        /// Gets or sets the Transact-SQL statement, table name or stored procedure to execute at the data source.
        /// </summary>
        string CommandText { get; set; }
        /// <summary>
        /// Gets or sets the wait time (seconds) before terminating the attempt to execute a command and generating an error.
        /// </summary>
        /// <remarks>
        /// A value of 0 indications no limit, and should be avoided in a CommandTimeout because an attempt to execute a command will wait indefinitely.
        /// </remarks>
        int CommandTimeout { get; set; }

        void Execute( IConnection connection );

        void Execute( IConnection connection, ITransaction transaction );

        object Scalar( IConnection connection );

        object Scalar( IConnection connection, ITransaction transaction );

        IRecordSet Query( IConnection connection );

        IRecordSet Query( IConnection connection, ITransaction transaction );

        IParameter GetParameter( string name );

        bool ContainsParameter( string name );

        void RemoveParameter( string name );

        ICommand SetCommandType( CommandType type );

        ICommand SetCommandText( string text );

        ICommand SetCommandTimeout( int timeout );

        ICommand AddParameter( string definition );

        ICommand AddParameter( string definition, object value );

        ICommand AddParameter( IParameter parameter );

        ICommand AddParameters( string definitions );

        ICommand AddParameters( string definitions, params object[] values );

        ICommand AddParameters( IEnumerable<IParameter> parameters );

        ICommand SetParameter( string name, object value );

        ICommand SetParameter( int index, object value );

        ICommand SetParameters( int startIndex, params object[] values );
    }
}