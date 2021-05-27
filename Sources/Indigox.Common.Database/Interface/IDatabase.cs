using System;
using System.Collections.Generic;

namespace Indigox.Common.Data.Interface
{
    public interface IDatabase
    {
        IConnectionString ConnectionString { get; }

        IConnection CreateConnection();

        ICommand CreateTextCommand();

        ICommand CreateTextCommand( string text );

        ICommand CreateStoredProcedureCommand();

        ICommand CreateStoredProcedureCommand( string text );

        ICommandBatch CreateCommandBatch();

        IParameter CreateParameter();

        IRecordSet Query( ICommand command );

        IRecordSet Query( IConnection connection, ICommand command );

        IRecordSet Query( IConnection connection, ITransaction transaction, ICommand command );

        IRecordSet QueryText( string text );

        IRecordSet QueryText( string text, string paramDefinitions, params object[] paramValues );

        IRecordSet QueryText( IConnection connection, string text );

        IRecordSet QueryText( IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        IRecordSet QueryText( IConnection connection, ITransaction transaction, string text );

        IRecordSet QueryText( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        IRecordSet QueryStoredProcedure( string text );

        IRecordSet QueryStoredProcedure( string text, string paramDefinitions, params object[] paramValues );

        IRecordSet QueryStoredProcedure( IConnection connection, string text );

        IRecordSet QueryStoredProcedure( IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        IRecordSet QueryStoredProcedure( IConnection connection, ITransaction transaction, string text );

        IRecordSet QueryStoredProcedure( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        IList<T> Query<T>( ICommand command );

        IList<T> Query<T>( IConnection connection, ICommand command );

        IList<T> Query<T>( IConnection connection, ITransaction transaction, ICommand command );

        IList<T> QueryText<T>( string text );

        IList<T> QueryText<T>( string text, string paramDefinitions, params object[] paramValues );

        IList<T> QueryText<T>( IConnection connection, string text );

        IList<T> QueryText<T>( IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        IList<T> QueryText<T>( IConnection connection, ITransaction transaction, string text );

        IList<T> QueryText<T>( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        IList<T> QueryStoredProcedure<T>( string text );

        IList<T> QueryStoredProcedure<T>( string text, string paramDefinitions, params object[] paramValues );

        IList<T> QueryStoredProcedure<T>( IConnection connection, string text );

        IList<T> QueryStoredProcedure<T>( IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        IList<T> QueryStoredProcedure<T>( IConnection connection, ITransaction transaction, string text );

        IList<T> QueryStoredProcedure<T>( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        IList<Object> Query( Type entityType, ICommand command );

        IList<Object> Query( Type entityType, IConnection connection, ICommand command );

        IList<Object> Query( Type entityType, IConnection connection, ITransaction transaction, ICommand command );

        IList<Object> QueryText( Type entityType, string text );

        IList<Object> QueryText( Type entityType, string text, string paramDefinitions, params object[] paramValues );

        IList<Object> QueryText( Type entityType, IConnection connection, string text );

        IList<Object> QueryText( Type entityType, IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        IList<Object> QueryText( Type entityType, IConnection connection, ITransaction transaction, string text );

        IList<Object> QueryText( Type entityType, IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        IList<Object> QueryStoredProcedure( Type entityType, string text );

        IList<Object> QueryStoredProcedure( Type entityType, string text, string paramDefinitions, params object[] paramValues );

        IList<Object> QueryStoredProcedure( Type entityType, IConnection connection, string text );

        IList<Object> QueryStoredProcedure( Type entityType, IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        IList<Object> QueryStoredProcedure( Type entityType, IConnection connection, ITransaction transaction, string text );

        IList<Object> QueryStoredProcedure( Type entityType, IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        void Execute( ICommand command );

        void Execute( IConnection connection, ICommand command );

        void Execute( IConnection connection, ITransaction transaction, ICommand command );

        void ExecuteText( string text );

        void ExecuteText( string text, string paramDefinitions, params object[] paramValues );

        void ExecuteText( IConnection connection, string text );

        void ExecuteText( IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        void ExecuteText( IConnection connection, ITransaction transaction, string text );

        void ExecuteText( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        void ExecuteStoredProcedure( string text );

        void ExecuteStoredProcedure( string text, string paramDefinitions, params object[] paramValues );

        void ExecuteStoredProcedure( IConnection connection, string text );

        void ExecuteStoredProcedure( IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        void ExecuteStoredProcedure( IConnection connection, ITransaction transaction, string text );

        void ExecuteStoredProcedure( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        object Scalar( ICommand command );

        object Scalar( IConnection connection, ICommand command );

        object Scalar( IConnection connection, ITransaction transaction, ICommand command );

        object ScalarText( string text );

        object ScalarText( string text, string paramDefinitions, params object[] paramValues );

        object ScalarText( IConnection connection, string text );

        object ScalarText( IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        object ScalarText( IConnection connection, ITransaction transaction, string text );

        object ScalarText( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        object ScalarStoredProcedure( string text );

        object ScalarStoredProcedure( string text, string paramDefinitions, params object[] paramValues );

        object ScalarStoredProcedure( IConnection connection, string text );

        object ScalarStoredProcedure( IConnection connection, string text, string paramDefinitions, params object[] paramValues );

        object ScalarStoredProcedure( IConnection connection, ITransaction transaction, string text );

        object ScalarStoredProcedure( IConnection connection, ITransaction transaction, string text, string paramDefinitions, params object[] paramValues );

        void ExecuteBatch( ICommandBatch commandBatch );

        void ExecuteBatch( ICommandBatch commandBatch, bool oneTransaction );

        void ExecuteBatch( ICommandBatch commandBatch, IConnection connection );

        void ExecuteBatch( ICommandBatch commandBatch, IConnection connection, bool oneTransaction );

        void ExecuteBatch( ICommandBatch commandBatch, IConnection connection, ITransaction transaction );
    }
}