using System;

namespace Indigox.Common.Data.Interface
{
    public interface IColumn
    {
        string Name { get; }
        string DbType { get; }
        bool Nullable { get; }
    }
}