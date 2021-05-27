using System;

namespace Indigox.Common.Data.Interface
{
    public interface IConnection
    {
        IConnectionString ConnectionString { get; }
        bool Connected { get; }

        void Open();

        void Close();

        ITransaction BeginTransaction();
    }
}