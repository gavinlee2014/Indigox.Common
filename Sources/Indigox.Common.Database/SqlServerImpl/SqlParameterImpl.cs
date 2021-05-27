using System;
using System.Data;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.SqlServerImpl
{
    internal class SqlParameterImpl : IParameter
    {
        private IDataParameter systemParameter;

        internal IDataParameter GetSystemParameter()
        {
            if ( systemParameter == null )
            {
                systemParameter = new System.Data.SqlClient.SqlParameter();
            }
            return systemParameter;
        }

        internal SqlDbType GetSystemParameterType()
        {
            switch ( this.DbType.ToLower() )
            {
                case "int":
                    return System.Data.SqlDbType.Int;

                case "float":
                    return System.Data.SqlDbType.Float; // System.Double, double

                case "real":
                    return System.Data.SqlDbType.Real; // System.Single, float

                case "decimal":
                    return System.Data.SqlDbType.Decimal; // System.Decimal, decimal

                case "tinyint":
                    return System.Data.SqlDbType.TinyInt;

                case "smallint":
                    return System.Data.SqlDbType.SmallInt;

                case "bigint":
                    return System.Data.SqlDbType.BigInt;

                case "binary":
                    return System.Data.SqlDbType.Binary;

                case "varbinary":
                    return System.Data.SqlDbType.VarBinary;

                case "bit":
                    return System.Data.SqlDbType.Bit;

                case "date":
                case "datetime":
                case "datetime2":
                    return System.Data.SqlDbType.DateTime;

                case "guid":
                case "uniqueidentifier":
                    return System.Data.SqlDbType.UniqueIdentifier;

                case "xml":
                    return System.Data.SqlDbType.Xml;

                case "text":
                    return System.Data.SqlDbType.Text;

                case "ntext":
                    return System.Data.SqlDbType.NText;

                case "char":
                    return System.Data.SqlDbType.Char;

                case "nchar":
                    return System.Data.SqlDbType.NChar;

                case "varchar":
                    return System.Data.SqlDbType.VarChar;

                case "nvarchar":
                default:
                    return System.Data.SqlDbType.NVarChar;
            }
        }

        internal System.Data.ParameterDirection GetSysteParameterDirection()
        {
            switch ( this.Direction )
            {
                case ParameterDirection.Input:
                    return System.Data.ParameterDirection.Input;
                case ParameterDirection.Output:
                    return System.Data.ParameterDirection.InputOutput;
                default:
                    throw new NotSupportedException( "ParameterDirection : " + this.Direction );
            }
        }

        public string Name
        {
            get;
            set;
        }

        public string DbType
        {
            get;
            set;
        }

        public int MaxLength
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }

        public ParameterDirection Direction
        {
            get;
            set;
        }
    }
}