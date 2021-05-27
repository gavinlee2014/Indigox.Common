using System;

namespace Indigox.Common.Data.Utils
{
    internal class SqlValueConvert
    {
        public static string ToSqlString( object value )
        {
            string retStr = "";
            if ( value == null )
            {
                // null
                retStr = "null";
            }
            else if (
                value is int || value is short || value is long ||
                value is uint || value is ushort || value is ulong ||
                value is double || value is float || value is decimal )
            {
                // 数字类型
                retStr = value.ToString();
            }
            else if ( value is bool )
            {
                // 是否类型
                retStr = ( (bool)value ) ? "1" : "0";
            }
            else if ( value is DateTime )
            {
                // 日期类型
                retStr = string.Format( "'{0}'", ( (DateTime)value ).ToString( "yyyy-MM-dd HH:mm:ss.fff" ) );
            }
            else if ( value is Guid )
            {
                // GUID类型
                retStr = string.Format( "'{0}'", ( (Guid)value ).ToString() );
            }
            else if ( value.GetType().IsEnum )
            {
                // ENUM
                retStr = string.Format( "{0}", Convert.ToInt32( value ) );
            }
            else
            {
                // 字符串和其它类型
                retStr = "N'"
                       + value.ToString()
                              .Replace( "'", "''" )
                    /*
                     * 用 +char(13)+char(10)+ 的方式代替换行符时，如果原本的字符串长度超过4000会被阶段，
                     * 除非主动将 nvarchar(n) 转成 nvarchar(max)
                     */
                    //.Replace( "\r\n", "'+char(13)+char(10)+N'" )
                    //.Replace( "\n", "'+char(13)+N'" )
                    //.Replace( "\n", "'+char(10)+N'" )
                       + "'";
            }
            return retStr;
        }
    }
}