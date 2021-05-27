using System;
using System.Data;
using Indigox.Common.Data.Interface;
using Indigox.Common.Data.Utils;

namespace Indigox.Common.Data.SqlServerImpl
{
    internal class SqlConnectionImpl : IConnection, IDisposable
    {
        private IConnectionString connectionString;
        private IDbConnection systemConnection;
        private Counter connectionOpened = new Counter();

        internal IDbConnection GetSystemConnection()
        {
            if ( this.systemConnection == null )
            {
                this.systemConnection = new System.Data.SqlClient.SqlConnection( this.connectionString.ConnectionString );
            }
            return this.systemConnection;
        }

        public SqlConnectionImpl( IConnectionString connectionString )
        {
            this.connectionString = connectionString;
        }

        public IConnectionString ConnectionString
        {
            get { return this.connectionString; }
        }

        public bool Connected
        {
            get { return this.connectionOpened.Value > 0; }
        }

        public void Open()
        {
            if ( !this.Connected )
            {
                GetSystemConnection().Open();
            }
            this.connectionOpened.Increment();
        }

        public void Close()
        {
            this.connectionOpened.Decrement();
            if ( !this.Connected )
            {
                GetSystemConnection().Close();
            }
        }

        public ITransaction BeginTransaction()
        {
            SqlTransactionImpl transaction = new SqlTransactionImpl( this );
            transaction.Begin();
            return transaction;
        }

        public void Dispose()
        {
            if ( this.Connected )
            {
                this.Close();
            }
        }
    }
}