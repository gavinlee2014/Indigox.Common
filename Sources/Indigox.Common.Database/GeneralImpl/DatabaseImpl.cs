using System;
using System.Collections.Generic;
using Indigox.Common.Data.Interface;
using Indigox.Common.Logging;

namespace Indigox.Common.Data.GeneralImpl
{
    internal class DatabaseImpl : IDatabase
    {
        internal IFactory factory;
        internal IConnectionString connectionString;

        public IConnectionString ConnectionString
        {
            get { return this.connectionString; }
        }

        public IConnection CreateConnection()
        {
            return factory.CreateConnection( this );
        }

        public ICommand CreateTextCommand()
        {
            ICommand command = factory.CreateCommand( this )
                .SetCommandType( CommandType.Text );
            return command;
        }

        public ICommand CreateTextCommand( string text )
        {
            ICommand command = factory.CreateCommand( this )
                .SetCommandType( CommandType.Text )
                .SetCommandText( text );
            return command;
        }

        public ICommand CreateStoredProcedureCommand()
        {
            ICommand command = factory.CreateCommand( this )
                .SetCommandType( CommandType.StoredProcedure );
            return command;
        }

        public ICommand CreateStoredProcedureCommand( string text )
        {
            ICommand command = factory.CreateCommand( this )
                .SetCommandType( CommandType.StoredProcedure )
                .SetCommandText( text );
            return command;
        }

        public ICommandBatch CreateCommandBatch()
        {
            return factory.CreateCommandBatch( this );
        }

        public IParameter CreateParameter()
        {
            return factory.CreateParameter( this );
        }

        #region QueryRecordSet

        public IRecordSet Query( ICommand command )
        {
            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Query( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public IRecordSet Query( IConnection connection, ICommand command )
        {
            return command.Query( connection );
        }

        public IRecordSet Query( IConnection connection, ITransaction transaction, ICommand command )
        {
            return command.Query( connection, transaction );
        }

        public IRecordSet QueryText( string text )
        {
            ICommand command = CreateTextCommand( text );
            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Query( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public IRecordSet QueryText( string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );

            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Query( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public IRecordSet QueryText( IConnection connection, string text )
        {
            ICommand command = CreateTextCommand( text );
            return command.Query( connection );
        }

        public IRecordSet QueryText( IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            return command.Query( connection );
        }

        public IRecordSet QueryText( IConnection connection, ITransaction transaction, string text )
        {
            ICommand command = CreateTextCommand( text );
            return command.Query( connection, transaction );
        }

        public IRecordSet QueryText( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            return command.Query( connection, transaction );
        }

        public IRecordSet QueryStoredProcedure( string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Query( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public IRecordSet QueryStoredProcedure( string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );

            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Query( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public IRecordSet QueryStoredProcedure( IConnection connection, string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            return command.Query( connection );
        }

        public IRecordSet QueryStoredProcedure( IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            return command.Query( connection );
        }

        public IRecordSet QueryStoredProcedure( IConnection connection, ITransaction transaction, string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            return command.Query( connection, transaction );
        }

        public IRecordSet QueryStoredProcedure( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            return command.Query( connection, transaction );
        }

        #endregion QueryRecordSet

        #region QueryGenericObject

        public IList<T> Query<T>( ICommand command )
        {
            throw new NotImplementedException();
        }

        public IList<T> Query<T>( IConnection connection, ICommand command )
        {
            throw new NotImplementedException();
        }

        public IList<T> Query<T>( IConnection connection, ITransaction transaction, ICommand command )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryText<T>( string text )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryText<T>( string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryText<T>( IConnection connection, string text )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryText<T>( IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryText<T>( IConnection connection, ITransaction transaction, string text )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryText<T>( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryStoredProcedure<T>( string text )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryStoredProcedure<T>( string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryStoredProcedure<T>( IConnection connection, string text )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryStoredProcedure<T>( IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryStoredProcedure<T>( IConnection connection, ITransaction transaction, string text )
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryStoredProcedure<T>( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        #endregion QueryGenericObject

        #region QueryObject

        public IList<object> Query( Type entityType, ICommand command )
        {
            throw new NotImplementedException();
        }

        public IList<object> Query( Type entityType, IConnection connection, ICommand command )
        {
            throw new NotImplementedException();
        }

        public IList<object> Query( Type entityType, IConnection connection, ITransaction transaction, ICommand command )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryText( Type entityType, string text )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryText( Type entityType, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryText( Type entityType, IConnection connection, string text )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryText( Type entityType, IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryText( Type entityType, IConnection connection, ITransaction transaction, string text )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryText( Type entityType, IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryStoredProcedure( Type entityType, string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            IRecordSet recordSet = command.Query( CreateConnection() );

            throw new NotImplementedException();
        }

        public IList<object> QueryStoredProcedure( Type entityType, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryStoredProcedure( Type entityType, IConnection connection, string text )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryStoredProcedure( Type entityType, IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryStoredProcedure( Type entityType, IConnection connection, ITransaction transaction, string text )
        {
            throw new NotImplementedException();
        }

        public IList<object> QueryStoredProcedure( Type entityType, IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            throw new NotImplementedException();
        }

        #endregion QueryObject

        #region Execute

        public void Execute( ICommand command )
        {
            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                command.Execute( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public void Execute( IConnection connection, ICommand command )
        {
            command.Execute( connection );
        }

        public void Execute( IConnection connection, ITransaction transaction, ICommand command )
        {
            command.Execute( connection, transaction );
        }

        public void ExecuteText( string text )
        {
            ICommand command = CreateTextCommand( text );
            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                command.Execute( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public void ExecuteText( string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );

            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                command.Execute( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public void ExecuteText( IConnection connection, string text )
        {
            ICommand command = CreateTextCommand( text );
            command.Execute( connection );
        }

        public void ExecuteText( IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            command.Execute( connection );
        }

        public void ExecuteText( IConnection connection, ITransaction transaction, string text )
        {
            ICommand command = CreateTextCommand( text );
            command.Execute( connection, transaction );
        }

        public void ExecuteText( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            command.Execute( connection, transaction );
        }

        public void ExecuteStoredProcedure( string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                command.Execute( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public void ExecuteStoredProcedure( string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );

            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                command.Execute( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public void ExecuteStoredProcedure( IConnection connection, string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.Execute( connection );
        }

        public void ExecuteStoredProcedure( IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            command.Execute( connection );
        }

        public void ExecuteStoredProcedure( IConnection connection, ITransaction transaction, string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.Execute( connection, transaction );
        }

        public void ExecuteStoredProcedure( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            command.Execute( connection, transaction );
        }

        #endregion Execute

        #region Scalar

        public object Scalar( ICommand command )
        {
            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Scalar( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public object Scalar( IConnection connection, ICommand command )
        {
            return command.Scalar( connection );
        }

        public object Scalar( IConnection connection, ITransaction transaction, ICommand command )
        {
            return command.Scalar( connection, transaction );
        }

        public object ScalarText( string text )
        {
            ICommand command = CreateTextCommand( text );
            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Scalar( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public object ScalarText( string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );

            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Scalar( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public object ScalarText( IConnection connection, string text )
        {
            ICommand command = CreateTextCommand( text );
            return command.Scalar( connection );
        }

        public object ScalarText( IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            return command.Scalar( connection );
        }

        public object ScalarText( IConnection connection, ITransaction transaction, string text )
        {
            ICommand command = CreateTextCommand( text );
            return command.Scalar( connection, transaction );
        }

        public object ScalarText( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            return command.Scalar( connection, transaction );
        }

        public object ScalarStoredProcedure( string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Scalar( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public object ScalarStoredProcedure( string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );

            IConnection connection = CreateConnection();
            connection.Open();
            try
            {
                return command.Scalar( connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public object ScalarStoredProcedure( IConnection connection, string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            return command.Scalar( connection );
        }

        public object ScalarStoredProcedure( IConnection connection, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            return command.Scalar( connection );
        }

        public object ScalarStoredProcedure( IConnection connection, ITransaction transaction, string text )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            return command.Scalar( connection, transaction );
        }

        public object ScalarStoredProcedure( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            return command.Scalar( connection, transaction );
        }

        #endregion Scalar

        #region ExecuteBatch

        public void ExecuteBatch( ICommandBatch commandBatch )
        {
            this.ExecuteBatch( commandBatch, false );
        }

        public void ExecuteBatch( ICommandBatch commandBatch, bool oneTransaction )
        {
            IConnection connection = this.CreateConnection();
            connection.Open();
            try
            {
                this.ExecuteBatch( commandBatch, connection );
            }
            finally
            {
                connection.Close();
            }
        }

        public void ExecuteBatch( ICommandBatch commandBatch, IConnection connection )
        {
            this.ExecuteBatch( commandBatch, connection, false );
        }

        public void ExecuteBatch( ICommandBatch commandBatch, IConnection connection, bool oneTransaction )
        {
            ITransaction transaction = connection.BeginTransaction();
            bool success = false;
            try
            {
                commandBatch.Execute( connection, transaction );
                transaction.Commit();
                success = true;
            }
            finally
            {
                if ( !success )
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch ( Exception ex )
                    {
                        Log.Error( "Rollback transaction failed." );
                        Log.Error( ex.ToString() );
                    }
                }
            }
        }

        public void ExecuteBatch( ICommandBatch commandBatch, IConnection connection, ITransaction transaction )
        {
            commandBatch.Execute( connection, transaction );
        }

        #endregion ExecuteBatch
    }
}