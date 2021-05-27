using System;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.SqlServerImpl
{
    internal class SqlConnectionStringImpl : IConnectionString
    {
        private System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();

        private string connectionString = "";

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
            set
            {
                this.builder.ConnectionString = value;
                this.connectionString = value;
            }
        }

        public string Database
        {
            get
            {
                return this.builder.InitialCatalog;
            }
            set
            {
                this.builder.InitialCatalog = value;
                this.connectionString = this.builder.ConnectionString;
            }
        }

        public string Server
        {
            get
            {
                return this.builder.DataSource;
            }
            set
            {
                this.builder.DataSource = value;
                this.connectionString = this.builder.ConnectionString;
            }
        }

        public string User
        {
            get
            {
                return this.builder.UserID;
            }
            set
            {
                this.builder.UserID = value;
                this.connectionString = this.builder.ConnectionString;
            }
        }

        public string Password
        {
            get
            {
                return this.builder.Password;
            }
            set
            {
                this.builder.Password = value;
                this.connectionString = this.builder.ConnectionString;
            }
        }

        public int Timeout
        {
            get
            {
                return this.builder.ConnectTimeout;
            }
            set
            {
                this.builder.ConnectTimeout = value;
                this.connectionString = this.builder.ConnectionString;
            }
        }

        public int MinPoolSize
        {
            get
            {
                return this.builder.MinPoolSize;
            }
            set
            {
                this.builder.MinPoolSize = value;
                this.connectionString = this.builder.ConnectionString;
            }
        }

        public int MaxPoolSize
        {
            get
            {
                return this.builder.MaxPoolSize;
            }
            set
            {
                this.builder.MaxPoolSize = value;
                this.connectionString = this.builder.ConnectionString;
            }
        }
    }
}