using System;
using Indigox.Common.Data.Interface;
using Indigox.Common.Data.Utils;

namespace Indigox.Common.Data.SqlServerImpl
{
    internal class SqlTransactionImpl : ITransaction
    {
        private SqlConnectionImpl connection;
        private System.Data.IDbTransaction systemTransaction;
        private Counter transactionDepth = new Counter();

        internal System.Data.IDbTransaction GetSystemTransaction()
        {
            if ( this.transactionDepth.Value == 0 )
            {
                throw new NotSupportedException( string.Format( "当 TransactionDepthCount = {0} 时，不能获取 IDbTransaction。", this.transactionDepth ) );
            }
            return this.systemTransaction;
        }

        internal void Begin()
        {
            if ( !this.connection.Connected )
            {
                throw new NotSupportedException( "必须在先连接到数据库才能开始事务。" );
            }
            if ( this.transactionDepth.Value == 0 )
            {
                this.systemTransaction = this.connection.GetSystemConnection().BeginTransaction();
            }
            this.transactionDepth.Increment();
        }

        public SqlTransactionImpl( SqlConnectionImpl connection )
        {
            this.connection = connection;
        }

        public void Commit()
        {
            this.transactionDepth.Decrement();
            if ( this.transactionDepth.Value == 0 )
            {
                this.systemTransaction.Commit();
            }
        }

        public void Rollback()
        {
            this.transactionDepth.Reset();
            this.systemTransaction.Rollback();
        }
    }
}