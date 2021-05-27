using System;
using Indigox.Common.Data;
using Indigox.Common.Data.Interface;

internal class Module
{
    private static IDatabase db;

    public static IDatabase UUV_DB
    {
        get
        {
            if ( db == null )
            {
                DatabaseFactory factory = new DatabaseFactory();
                db = factory.CreateDatabase( "UUV" );
            }
            return db;
        }
    }
    public static IDatabase BPM_DB
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

    public delegate void TransactionHandler( IConnection connection, ITransaction transaction );
}