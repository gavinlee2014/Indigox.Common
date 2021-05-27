using System;
using System.Data;
using Indigox.Common.Data.GeneralImpl;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.Utils
{
    internal class DbUtil
    {
        public static void FillRecordSet( IRecordSet recordSet, IDataReader reader )
        {
            int fieldCount = reader.FieldCount;
            for ( int i = 0; i < fieldCount; i++ )
            {
                string field = reader.GetName( i );
                recordSet.Columns.Add( new ColumnImpl( field ) );
            }

            while ( reader.Read() )
            {
                IRecord record = recordSet.NewRecord();
                for ( int i = 0; i < fieldCount; i++ )
                {
                    string field = reader.GetName( i );
                    object value = ( reader.IsDBNull( i ) ) ? null : reader.GetValue( i );
                    record.SetValue( field, value );
                }
                recordSet.Records.Add( record );
            }
        }

        public static void SetCommandType( IDbCommand command, CommandType commandType )
        {
            switch ( commandType )
            {
                case CommandType.Text:
                    command.CommandType = System.Data.CommandType.Text;
                    break;

                case CommandType.StoredProcedure:
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    break;
                default:
                    throw new NotSupportedException( "CommandType : " + commandType.ToString() );
            }
        }
    }
}