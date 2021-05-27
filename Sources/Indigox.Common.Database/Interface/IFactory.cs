using System;

namespace Indigox.Common.Data.Interface
{
    public interface IFactory
    {
        IConnectionString CreateConnectionString();

        IConnection CreateConnection( IDatabase db );

        ICommand CreateCommand( IDatabase db );

        ICommandBatch CreateCommandBatch( IDatabase db );

        IParameter CreateParameter( IDatabase db );
    }
}