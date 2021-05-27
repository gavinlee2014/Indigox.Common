using System;
using System.Collections.Generic;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.SqlServerImpl
{
    internal class SqlCommandBatchImpl : ICommandBatch
    {
        private IDatabase db;
        private IList<ICommand> commands = new List<ICommand>();

        public SqlCommandBatchImpl( IDatabase db )
        {
            this.db = db;
        }

        public IList<ICommand> Commands
        {
            get
            {
                return this.commands;
            }
        }

        public void Execute( IConnection connection )
        {
            foreach ( ICommand command in this.Commands )
            {
                command.Execute( connection );
            }
        }

        public void Execute( IConnection connection, ITransaction transaction )
        {
            foreach ( ICommand command in this.Commands )
            {
                command.Execute( connection, transaction );
            }
        }

        public ICommandBatch AddCommand( ICommand command )
        {
            this.commands.Add( command );
            return this;
        }

        public ICommandBatch AddTextCommand( string text )
        {
            ICommand command = db.CreateTextCommand( text );
            this.commands.Add( command );
            return this;
        }

        public ICommandBatch AddTextCommand( string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = db.CreateTextCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            this.commands.Add( command );
            return this;
        }

        public ICommandBatch AddStoredProcedureCommand( string text )
        {
            ICommand command = db.CreateStoredProcedureCommand( text );
            this.commands.Add( command );
            return this;
        }

        public ICommandBatch AddStoredProcedureCommand( string text, string paramDefinitions, params object[] paramValues )
        {
            ICommand command = db.CreateStoredProcedureCommand( text );
            command.AddParameters( paramDefinitions, paramValues );
            this.commands.Add( command );
            return this;
        }
    }
}