using System;
using System.Data;
using Indigox.Common.Data.Interface;
using Indigox.Common.Data.Utils;

namespace Indigox.Common.Data.SqlServerImpl
{
    internal class SqlCommandBuilder
    {
        private IConnection connection;
        private ITransaction transaction;
        private ICommand command;

        public SqlCommandBuilder( IConnection connection, ICommand command )
        {
            this.connection = connection;
            this.transaction = null;
            this.command = command;
        }

        public SqlCommandBuilder( IConnection connection, ITransaction transaction, ICommand command )
        {
            this.connection = connection;
            this.transaction = transaction;
            this.command = command;
        }

        public IDbCommand BuildSystemCommand()
        {
            IDbCommand systemCommand = new System.Data.SqlClient.SqlCommand();
            BuildUsingCommand( systemCommand );
            BuildUsingConnection( systemCommand );
            if ( transaction != null )
            {
                BuildUsingTransaction( systemCommand );
            }
            return systemCommand;
        }

        protected virtual void BuildUsingCommand( IDbCommand systemCommand )
        {
            DbUtil.SetCommandType( systemCommand, command.CommandType );
            systemCommand.CommandText = command.CommandText;
            systemCommand.CommandTimeout = command.CommandTimeout;

            systemCommand.Parameters.Clear();
            if ( command.Parameters.Count > 0 )
            {
                foreach ( SqlParameterImpl parameter in command.Parameters )
                {
                    if ( parameter.Name.StartsWith( "$" ) )
                    {
                        systemCommand.CommandText = systemCommand.CommandText.Replace( parameter.Name, (string)parameter.Value );
                    }
                    else
                    {
                        System.Data.SqlClient.SqlParameter systemParameter = (System.Data.SqlClient.SqlParameter)parameter.GetSystemParameter();
                        systemParameter.ParameterName = parameter.Name;
                        systemParameter.SqlDbType = parameter.GetSystemParameterType();
                        systemParameter.Direction = parameter.GetSysteParameterDirection();
                        systemParameter.Value = ( parameter.Value == null ) ? DBNull.Value : parameter.Value;
                        systemCommand.Parameters.Add( systemParameter );
                    }
                }
            }
        }

        protected virtual void BuildUsingConnection( IDbCommand systemCommand )
        {
            SqlConnectionImpl sqlConnection = (SqlConnectionImpl)connection;
            systemCommand.Connection = sqlConnection.GetSystemConnection();
        }

        protected virtual void BuildUsingTransaction( IDbCommand systemCommand )
        {
            SqlTransactionImpl sqlTransaction = (SqlTransactionImpl)transaction;
            systemCommand.Transaction = sqlTransaction.GetSystemTransaction();
        }
    }
}