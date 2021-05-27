using System.Threading;
using Indigox.Common.Logging;
using Indigox.Common.StateContainer.StateContexts;

namespace Indigox.Common.StateContainer
{
    internal class CurrentSessionContextFactory
    {
        public static IStateContext Create()
        {
            if ( ApplicationInfo.IsWebApplication )
            {
                Log.Debug( string.Format( "[{0}] Create CurrentSessionBinder: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    typeof( WebStateContext ).FullName ) );
                return new WebStateContext();
            }
            else
            {
                Log.Debug( string.Format( "[{0}] Create CurrentSessionBinder: {1}",
                    Thread.CurrentThread.ManagedThreadId,
                    typeof( StaticStateContext ).FullName ) );
                return new StaticStateContext();
            }
        }
    }
}