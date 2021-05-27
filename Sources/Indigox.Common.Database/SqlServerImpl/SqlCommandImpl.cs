using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Indigox.Common.Data.GeneralImpl;
using Indigox.Common.Data.Interface;
using Indigox.Common.Data.Logging;
using Indigox.Common.Data.Utils;
using Indigox.Common.Logging;

namespace Indigox.Common.Data.SqlServerImpl
{
    internal class SqlCommandImpl : ICommand
    {
        private IDatabase db;
        private IList<IParameter> parameters;
        private CommandType commandType;
        private string commandText;
        private int commandTimeout;

        public SqlCommandImpl( IDatabase db )
        {
            this.db = db;
            this.parameters = new List<IParameter>();
            this.commandTimeout = 180;
        }

        public IList<IParameter> Parameters
        {
            get { return this.parameters; }
        }

        public CommandType CommandType
        {
            get { return this.commandType; }
            set { this.commandType = value; }
        }

        public string CommandText
        {
            get { return this.commandText; }
            set { this.commandText = value; }
        }

        public int CommandTimeout
        {
            get { return this.commandTimeout; }
            set { this.commandTimeout = value; }
        }

        public void Execute( IConnection connection )
        {
            Log.Debug( ObjectMessageFormater.Format( this ) );

            SqlCommandBuilder builder = new SqlCommandBuilder( connection, this );
            using ( IDbCommand systemCommand = builder.BuildSystemCommand() )
            {
                try
                {
                    systemCommand.ExecuteNonQuery();
                }
                catch ( DbException ex )
                {
                    throw WrapDbException( ex, this, systemCommand );
                }
                this.RefreshOutputParameters( systemCommand );
            }
        }

        public void Execute( IConnection connection, ITransaction transaction )
        {
            Log.Debug( ObjectMessageFormater.Format( this ) );

            SqlCommandBuilder builder = new SqlCommandBuilder( connection, transaction, this );
            using ( IDbCommand systemCommand = builder.BuildSystemCommand() )
            {
                try
                {
                    systemCommand.ExecuteNonQuery();
                }
                catch ( DbException ex )
                {
                    throw WrapDbException( ex, this, systemCommand );
                }
                this.RefreshOutputParameters( systemCommand );
            }
        }

        public object Scalar( IConnection connection )
        {
            Log.Debug( ObjectMessageFormater.Format( this ) );

            object value = null;

            SqlCommandBuilder builder = new SqlCommandBuilder( connection, this );
            using ( IDbCommand systemCommand = builder.BuildSystemCommand() )
            {
                try
                {
                    value = systemCommand.ExecuteScalar();
                    if ( value == DBNull.Value )
                    {
                        value = null;
                    }
                }
                catch ( DbException ex )
                {
                    throw WrapDbException( ex, this, systemCommand );
                }
                this.RefreshOutputParameters( systemCommand );
            }

            return value;
        }

        public object Scalar( IConnection connection, ITransaction transaction )
        {
            Log.Debug( ObjectMessageFormater.Format( this ) );

            object value = null;

            SqlCommandBuilder builder = new SqlCommandBuilder( connection, transaction, this );
            using ( IDbCommand systemCommand = builder.BuildSystemCommand() )
            {
                try
                {
                    value = systemCommand.ExecuteScalar();
                    if ( value == DBNull.Value )
                    {
                        value = null;
                    }
                }
                catch ( DbException ex )
                {
                    throw WrapDbException( ex, this, systemCommand );
                }
                this.RefreshOutputParameters( systemCommand );
            }

            return value;
        }

        public IRecordSet Query( IConnection connection )
        {
            Log.Debug( ObjectMessageFormater.Format( this ) );

            IRecordSet recordSet = new RecordSetImpl();
            IDataReader reader = null;
            SqlCommandBuilder builder = new SqlCommandBuilder( connection, this );
            using ( IDbCommand systemCommand = builder.BuildSystemCommand() )
            {
                try
                {
                    reader = systemCommand.ExecuteReader();
                }
                catch ( DbException ex )
                {
                    throw WrapDbException( ex, this, systemCommand );
                }

                try
                {
                    DbUtil.FillRecordSet( recordSet, reader );
                }
                finally
                {
                    reader.Close();
                }
                this.RefreshOutputParameters( systemCommand );
            }
            return recordSet;
        }

        public IRecordSet Query( IConnection connection, ITransaction transaction )
        {
            Log.Debug( ObjectMessageFormater.Format( this ) );

            IRecordSet recordSet = new RecordSetImpl();
            IDataReader reader = null;
            SqlCommandBuilder builder = new SqlCommandBuilder( connection, transaction, this );
            using ( IDbCommand systemCommand = builder.BuildSystemCommand() )
            {
                try
                {
                    reader = systemCommand.ExecuteReader();
                }
                catch ( DbException ex )
                {
                    throw WrapDbException( ex, this, systemCommand );
                }
                try
                {
                    DbUtil.FillRecordSet( recordSet, reader );
                }
                finally
                {
                    reader.Close();
                }
                this.RefreshOutputParameters( systemCommand );
            }

            return recordSet;
        }

        public IParameter GetParameter( string name )
        {
            IParameter parameter = BaseGetParameter( name );
            if ( parameter != null )
            {
                return parameter;
            }
            throw new OverflowException( "Parameter name : " + name );
        }

        public bool ContainsParameter( string name )
        {
            IParameter parameter = BaseGetParameter( name );
            return parameter != null;
        }

        public void RemoveParameter( string name )
        {
            IParameter parameter = BaseGetParameter( name );
            this.parameters.Remove( parameter );
        }

        public ICommand SetCommandType( CommandType type )
        {
            this.CommandType = type;
            return this;
        }

        public ICommand SetCommandText( string text )
        {
            this.CommandText = text;
            return this;
        }

        public ICommand SetCommandTimeout( int timeout )
        {
            this.CommandTimeout = timeout;
            return this;
        }

        public ICommand AddParameter( string definition )
        {
            IParameter parameter = ParameterBuilder.GetParameter( this.db, definition );
            this.parameters.Add( parameter );
            return this;
        }

        public ICommand AddParameter( string definition, object value )
        {
            IParameter parameter = ParameterBuilder.GetParameter( this.db, definition );
            parameter.Value = value;
            this.parameters.Add( parameter );
            return this;
        }

        public ICommand AddParameters( string definitions )
        {
            IList<IParameter> parameterList = ParameterBuilder.GetParameters( this.db, definitions );
            foreach ( IParameter parameter in parameterList )
            {
                this.parameters.Add( parameter );
            }
            return this;
        }

        public ICommand AddParameters( string definitions, params object[] values )
        {
            IList<IParameter> parameterList = ParameterBuilder.GetParameters( this.db, definitions, values );
            foreach ( IParameter parameter in parameterList )
            {
                this.parameters.Add( parameter );
            }
            return this;
        }

        public ICommand AddParameter( IParameter parameter )
        {
            this.parameters.Add( parameter );
            return this;
        }

        public ICommand AddParameters( IEnumerable<IParameter> parameters )
        {
            foreach ( IParameter parameter in parameters )
            {
                this.parameters.Add( parameter );
            }
            return this;
        }

        public ICommand SetParameter( string name, object value )
        {
            this.GetParameter( name ).Value = value;
            return this;
        }

        public ICommand SetParameter( int index, object value )
        {
            this.parameters[ index ].Value = value;
            return this;
        }

        public ICommand SetParameters( int startIndex, params object[] values )
        {
            if ( this.parameters.Count < startIndex + values.Length )
            {
                throw new OverflowException( "Index overflow." );
            }
            for ( int i = 0; i < values.Length; i++ )
            {
                this.parameters[ startIndex + i ].Value = values[ i ];
            }
            return this;
        }

        private IParameter BaseGetParameter( string name )
        {
            foreach ( IParameter parameter in parameters )
            {
                if ( string.Equals( parameter.Name, name, StringComparison.CurrentCultureIgnoreCase ) )
                {
                    return parameter;
                }
            }
            return null;
        }

        private void RefreshOutputParameters( IDbCommand systemCommand )
        {
            foreach ( IParameter parameter in parameters )
            {
                if ( parameter.Direction == ParameterDirection.Output )
                {
                    object value = ( (IDbDataParameter)systemCommand.Parameters[ parameter.Name ] ).Value;
                    parameter.Value = ( value == DBNull.Value ) ? null : value;
                }
            }
        }

        private static Exception WrapDbException( DbException ex, ICommand command, System.Data.IDbCommand systemCommand )
        {
            Log.Error( ex.ToString() );
            //Log.Error( ObjectMessageFormater.Format( systemCommand ) );
            Log.Error( ObjectMessageFormater.Format( command ) );
#if DEBUG
            Indigox.Common.Data.Logging.IMessageFormater format = new Indigox.Common.Data.Logging.MessageFormaters.CommandMessageFormater();
            string detail = format.Format( command );
            return new Exception( "数据库操作错误，详情请查看日志文件。\r\n" + detail, ex );
#else
            return new Exception( "数据库操作错误，详情请查看日志文件。" );
#endif
        }
    }
}