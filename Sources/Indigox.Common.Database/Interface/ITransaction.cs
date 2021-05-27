using System;

namespace Indigox.Common.Data.Interface
{
    public interface ITransaction
    {
        void Commit();

        void Rollback();
    }
}