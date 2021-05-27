using System;
using System.Collections.Generic;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.GeneralImpl
{
    internal class ArrayRecordImpl : BaseRecordImpl
    {
        private object[] data;
        private Dictionary<string, int> columnsIndex;

        public ArrayRecordImpl( IRecordSet recordSet )
            : base( recordSet )
        {
            this.BuildIndex( out this.data, out this.columnsIndex );
        }

        public override object[] GetValues()
        {
            return data;
        }

        public override object GetValue( string column )
        {
            if ( !this.ContainsColumn( column ) )
            {
                if ( this.recordSet.ContainsColumn( column ) )
                {
                    this.RebuildIndex();
                }
                throw new OverflowException( "Column : " + column );
            }
            return data[ columnsIndex[ column ] ];
        }

        public override void SetValue( string column, object value )
        {
            if ( !this.ContainsColumn( column ) )
            {
                if ( this.recordSet.ContainsColumn( column ) )
                {
                    this.RebuildIndex();
                }
                throw new OverflowException( "Column : " + column );
            }
            data[ columnsIndex[ column ] ] = value;
        }

        private void BuildIndex( out object[] data, out Dictionary<string, int> columnsIndex )
        {
            data = new object[ recordSet.Columns.Count ];
            columnsIndex = new Dictionary<string, int>();
            for ( int i = 0; i < this.recordSet.Columns.Count; i++ )
            {
                columnsIndex.Add( this.recordSet.Columns[ i ].Name, i );
            }
        }

        private void RebuildIndex()
        {
            object[] newData;
            Dictionary<string, int> newColumnsIndex;
            this.BuildIndex( out newData, out newColumnsIndex );
            foreach ( KeyValuePair<string, int> index in newColumnsIndex )
            {
                if ( this.ContainsColumn( index.Key ) )
                {
                    newData[ index.Value ] = GetValue( index.Key );
                }
            }
            this.data = newData;
            this.columnsIndex = newColumnsIndex;
        }

        private bool ContainsColumn( string column )
        {
            return this.columnsIndex.ContainsKey( column );
        }
    }
}