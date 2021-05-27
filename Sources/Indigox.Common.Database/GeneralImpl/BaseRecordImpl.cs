using System;
using System.Collections.Generic;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.GeneralImpl
{
    internal abstract class BaseRecordImpl : IRecord
    {
        protected IRecordSet recordSet;

        public BaseRecordImpl( IRecordSet recordSet )
        {
            this.recordSet = recordSet;
        }

        public IList<IColumn> Columns
        {
            get { return recordSet.Columns; }
        }

        public abstract object[] GetValues();

        public abstract object GetValue( string column );

        public abstract void SetValue( string column, object value );

        public int GetInt( string column )
        {
            object value = GetValue( column );
            if ( value == null )
            {
                return 0;
            }
            else
            {
                return (int)value;
            }
        }

        public long GetLong( string column )
        {
            object value = GetValue( column );
            if ( value == null )
            {
                return 0;
            }
            else
            {
                return (long)value;
            }
        }

        public double GetDouble( string column )
        {
            object value = GetValue( column );
            if ( value == null )
            {
                return 0;
            }
            else
            {
                return (double)value;
            }
        }

        public decimal GetDecimal( string column )
        {
            object value = GetValue( column );
            if ( value == null )
            {
                return 0;
            }
            else
            {
                return (decimal)value;
            }
        }

        public string GetString( string column )
        {
            object value = GetValue( column );
            return (string)value;
        }

        public bool GetBoolean( string column )
        {
            object value = GetValue( column );
            if ( value == null )
            {
                return false;
            }
            else
            {
                return (bool)value;
            }
        }

        public Guid GetGuid( string column )
        {
            object value = GetValue( column );
            if ( value == null )
            {
                return Guid.Empty;
            }
            else
            {
                return (Guid)value;
            }
        }

        public DateTime GetDateTime( string column )
        {
            object value = GetValue( column );
            if ( value == null )
            {
                return DateTime.MinValue;
            }
            else
            {
                return (DateTime)value;
            }
        }
    }
}