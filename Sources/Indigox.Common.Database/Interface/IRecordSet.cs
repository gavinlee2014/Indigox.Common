using System;
using System.Collections.Generic;

namespace Indigox.Common.Data.Interface
{
    public interface IRecordSet
    {
        IList<IColumn> Columns { get; }
        IList<IRecord> Records { get; }

        IRecord NewRecord();

        bool ContainsColumn( string name );

        IRecordSet AddColumn( string name );

        IRecordSet AddColumn( string name, string dbType );

        IRecordSet AddColumn( string name, string dbType, bool nullable );
    }
}