using System;

namespace Indigox.Common.Data.Interface
{
    public interface IParameter
    {
        string Name { get; set; }
        string DbType { get; set; }
        int MaxLength { get; set; }
        object Value { get; set; }
        ParameterDirection Direction { get; set; }
    }
}