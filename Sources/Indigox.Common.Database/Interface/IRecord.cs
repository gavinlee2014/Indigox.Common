using System;
using System.Collections.Generic;

namespace Indigox.Common.Data.Interface
{
    public interface IRecord
    {
        IList<IColumn> Columns { get; }

        object[] GetValues();

        object GetValue( string column );

        void SetValue( string column, object value );

        int GetInt( string column );

        long GetLong( string column );

        double GetDouble( string column );

        decimal GetDecimal( string column );

        string GetString( string column );

        bool GetBoolean( string column );

        Guid GetGuid( string column );

        DateTime GetDateTime( string column );
    }
}