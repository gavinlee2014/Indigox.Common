using System;
using System.Text;
using Indigox.Common.Data.Utils;

namespace Indigox.Common.Data.Logging.MessageFormaters
{
    internal class SystemCommandMessageFormater : IMessageFormater
    {
        public bool IsMatch( object msg )
        {
            return ( msg is System.Data.IDbCommand );
        }

        public string Format( object msg )
        {
            StringBuilder builder = new StringBuilder();

            System.Data.IDbCommand systemCommand = msg as System.Data.IDbCommand;
            builder.AppendLine();
            builder.Append( "Type             : " + systemCommand.GetType().FullName );
            builder.AppendLine();
            builder.Append( "CommandType      : " + systemCommand.CommandType );
            builder.AppendLine();
            builder.Append( "CommandText      : " + systemCommand.CommandText );
            builder.AppendLine();
            builder.Append( "Parameters       : " );
            AppendParameterDefinitions( builder, systemCommand );
            builder.AppendLine();
            builder.Append( "Parameters Value : " );
            AppendParameterValues( builder, systemCommand );
            builder.AppendLine();

            return builder.ToString();
        }

        private void AppendParameterValues( StringBuilder builder, System.Data.IDbCommand systemCommand )
        {
            if ( systemCommand.Parameters.Count > 0 )
            {
                foreach ( System.Data.IDataParameter systemParameter in systemCommand.Parameters )
                {
                    builder.Append( string.Format( "{0} = {1}, ", systemParameter.ParameterName, SqlValueConvert.ToSqlString( systemParameter.Value ) ) );
                }
            }
        }

        private void AppendParameterDefinitions( StringBuilder builder, System.Data.IDbCommand systemCommand )
        {
            if ( systemCommand.Parameters.Count > 0 )
            {
                foreach ( System.Data.IDataParameter systemParameter in systemCommand.Parameters )
                {
                    builder.Append( string.Format( "{0} {1} {2}, ", systemParameter.ParameterName, systemParameter.DbType, systemParameter.Direction ) );
                }
            }
        }
    }
}