
namespace Indigox.Common.Data.Logging
{
    class IndigoLogger : ILogger
    {
        public void Debug( object msg )
        {
            Indigox.Common.Logging.Log.Debug( ObjectMessageFormater.Format( msg ) );
        }

        public void Info( object msg )
        {
            Indigox.Common.Logging.Log.Info( ObjectMessageFormater.Format( msg ) );
        }

        public void Error( object msg )
        {
            Indigox.Common.Logging.Log.Error( ObjectMessageFormater.Format( msg ) );
        }
    }
}
