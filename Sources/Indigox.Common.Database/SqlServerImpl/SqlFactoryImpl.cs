using System;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.SqlServerImpl
{
    internal class SqlFactoryImpl : IFactory
    {
        public IConnectionString CreateConnectionString()
        {
            return new SqlConnectionStringImpl();
        }

        public IConnection CreateConnection( IDatabase db )
        {
            return new SqlConnectionImpl( db.ConnectionString );
        }

        public ICommand CreateCommand( IDatabase db )
        {
            return new SqlCommandImpl( db );
        }

        public ICommandBatch CreateCommandBatch( IDatabase db )
        {
            return new SqlCommandBatchImpl( db );
        }

        public IParameter CreateParameter( IDatabase db )
        {
            return new SqlParameterImpl();
        }
    }
}