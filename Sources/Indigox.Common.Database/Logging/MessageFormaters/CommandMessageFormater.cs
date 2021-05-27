using System;
using System.Text;
using Indigox.Common.Data.Interface;
using Indigox.Common.Data.Utils;

namespace Indigox.Common.Data.Logging.MessageFormaters
{
    internal class CommandMessageFormater : IMessageFormater
    {
        public bool IsMatch( object msg )
        {
            return ( msg is ICommand );
        }

        public string Format( object msg )
        {
            StringBuilder builder = new StringBuilder();

            ICommand command = msg as ICommand;
            builder.AppendLine();
            builder.Append( "Type              : " + command.GetType().FullName );
            builder.AppendLine();
            builder.Append( "CommandType       : " + command.CommandType );
            builder.AppendLine();
            builder.Append( "CommandText       : " + command.CommandText );
            builder.AppendLine();
            builder.Append( "Parameters        : " );
            AppendParameterDefinitions( builder, command );
            builder.AppendLine();
            builder.Append( "Parameters Values : " );
            AppendParameterValues( builder, command );
            builder.AppendLine();

            return builder.ToString();
        }

        private void AppendParameterValues( StringBuilder builder, ICommand command )
        {
            if ( command.Parameters.Count > 0 )
            {
                foreach ( IParameter parameter in command.Parameters )
                {
                    builder.Append( string.Format( "{0} = {1}, ", parameter.Name, SqlValueConvert.ToSqlString( parameter.Value ) ) );
                }
            }
        }

        private void AppendParameterDefinitions( StringBuilder builder, ICommand command )
        {
            if ( command.Parameters.Count > 0 )
            {
                foreach ( IParameter parameter in command.Parameters )
                {
                    builder.Append( string.Format( "{0} {1} {2}, ", parameter.Name, parameter.DbType, parameter.Direction ) );
                }
            }
        }
    }
}