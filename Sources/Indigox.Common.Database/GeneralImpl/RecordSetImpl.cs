using System;
using System.Collections.Generic;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.GeneralImpl
{
    internal class RecordSetImpl : IRecordSet
    {
        private IList<IColumn> columns = new List<IColumn>();
        private IList<IRecord> records = new List<IRecord>();

        public IList<IColumn> Columns
        {
            get { return this.columns; }
        }

        public IList<IRecord> Records
        {
            get { return this.records; }
        }

        public IRecord NewRecord()
        {
            /**
             * ArrayRecordImpl 的遍历比 HashtableRecordImpl 快，能基本保持与 DataSet 差不多的时间
             */
            return new ArrayRecordImpl( this );
        }

        public bool ContainsColumn( string name )
        {
            foreach ( IColumn column in columns )
            {
                if ( string.Equals( column.Name, name, StringComparison.CurrentCultureIgnoreCase ) )
                {
                    return true;
                }
            }
            return false;
        }

        public IRecordSet AddColumn( string name )
        {
            this.columns.Add( new ColumnImpl( name ) );
            return this;
        }

        public IRecordSet AddColumn( string name, string dbType )
        {
            this.columns.Add( new ColumnImpl( name, dbType ) );
            return this;
        }

        public IRecordSet AddColumn( string name, string dbType, bool nullable )
        {
            this.columns.Add( new ColumnImpl( name, dbType, nullable ) );
            return this;
        }
    }
}