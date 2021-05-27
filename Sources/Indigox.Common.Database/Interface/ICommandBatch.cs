using System;
using System.Collections.Generic;

namespace Indigox.Common.Data.Interface
{
    public interface ICommandBatch
    {
        IList<ICommand> Commands { get; }

        void Execute( IConnection connection );

        void Execute( IConnection connection, ITransaction transaction );

        ICommandBatch AddCommand( ICommand command );

        ICommandBatch AddTextCommand( string text );

        ICommandBatch AddTextCommand( string text, string paramDefinitions, params object[] paramValues );

        ICommandBatch AddStoredProcedureCommand( string text );

        ICommandBatch AddStoredProcedureCommand( string text, string paramDefinitions, params object[] paramValues );
    }
}