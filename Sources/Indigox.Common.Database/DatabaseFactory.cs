using System;
using System.Data.Common;
using Indigox.Common.Data.Configuration;
using Indigox.Common.Data.GeneralImpl;
using Indigox.Common.Data.Interface;
using Indigox.Common.Data.SqlServerImpl;

namespace Indigox.Common.Data
{
    public class DatabaseFactory
    {
        public IDatabase CreateDatabase( string name )
        {
            ConnectionElement connectionElement = DatabaseSection.Default.Connections[ name ];
            if ( connectionElement == null )
            {
                throw new Exception( "找不到数据库连接配置[" + name + "]" );
            }
            return CreateDatabase( connectionElement.ConnectionString, connectionElement.Provider );
        }

        public IDatabase CreateDatabase( string connectionString, string providerName )
        {
            IFactory _factory = GetFactory( providerName );
            IConnectionString _connectionString = _factory.CreateConnectionString();
            _connectionString.ConnectionString = connectionString;

            DatabaseImpl db = new DatabaseImpl();
            db.factory = _factory;
            db.connectionString = _connectionString;
            return db;
        }

        private IFactory GetFactory( string providerName )
        {
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory( providerName );
            if ( dbProviderFactory is System.Data.SqlClient.SqlClientFactory )
            {
                return new SqlFactoryImpl();
            }
            else
            {
                throw new NotSupportedException( "ProviderName : " + providerName );
            }
        }
    }
}