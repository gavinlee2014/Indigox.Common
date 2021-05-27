using System;
using Indigox.Common.Data;
using Indigox.Common.Data.Interface;

internal class Module
{
    private static IDatabase db;

    public static IDatabase Db
    {
        get
        {
            if ( db == null )
            {
                DatabaseFactory factory = new DatabaseFactory();
                db = factory.CreateDatabase( "BPM" );
            }
            return db;
        }
    }

    public static void DoTransaction( TransactionHandler handler )
    {
        IConnection connection = Module.Db.CreateConnection();
        connection.Open();
        try
        {
            ITransaction transaction = connection.BeginTransaction();
            bool transactionCompleted = false;
            try
            {
                handler.Invoke( connection, transaction );
                transactionCompleted = true;
            }
            finally
            {
                if ( transactionCompleted )
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
        }
        finally
        {
            connection.Close();
        }
    }

    public delegate void TransactionHandler( IConnection connection, ITransaction transaction );
}