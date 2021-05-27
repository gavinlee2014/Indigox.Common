using System;
using System.Collections;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.GeneralImpl
{
    internal class HashtableRecordImpl : BaseRecordImpl
    {
        private Hashtable keyValues = new Hashtable( StringComparer.CurrentCultureIgnoreCase );

        public HashtableRecordImpl( IRecordSet recordSet )
            : base( recordSet )
        {
        }

        public override object[] GetValues()
        {
            object[] values = new object[ recordSet.Columns.Count ];
            for ( int i = 0; i < values.Length; i++ )
            {
                values[ i ] = this.GetValue( recordSet.Columns[ i ].Name );
            }
            return values;
        }

        public override object GetValue( string column )
        {
            if ( !this.recordSet.ContainsColumn( column ) )
            {
                throw new OverflowException( "Column : " + column );
            }
            return keyValues[ column ];
        }

        public override void SetValue( string column, object value )
        {
            if ( !this.recordSet.ContainsColumn( column ) )
            {
                throw new OverflowException( "Column : " + column );
            }
            keyValues[ column ] = value;
        }
    }
}