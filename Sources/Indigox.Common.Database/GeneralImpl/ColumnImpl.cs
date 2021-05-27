using System;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.GeneralImpl
{
    internal class ColumnImpl : IColumn
    {
        public ColumnImpl( string name )
            : this( name, "varchar", true )
        {
        }

        public ColumnImpl( string name, string dbType )
            : this( name, dbType, true )
        {
        }

        public ColumnImpl( string name, string dbType, bool nullable )
        {
            this.name = name;
            this.dbType = dbType;
            this.nullable = nullable;
        }

        private string name;
        private string dbType;
        private bool nullable;

        public string Name
        {
            get { return this.name; }
        }

        public string DbType
        {
            get { return this.dbType; }
        }

        public bool Nullable
        {
            get { return this.nullable; }
        }
    }
}