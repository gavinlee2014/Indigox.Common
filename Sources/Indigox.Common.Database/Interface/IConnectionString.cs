using System;

namespace Indigox.Common.Data.Interface
{
    public interface IConnectionString
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
        string Server { get; set; }
        string User { get; set; }
        string Password { get; set; }
        int Timeout { get; set; }
        int MinPoolSize { get; set; }
        int MaxPoolSize { get; set; }
    }
}