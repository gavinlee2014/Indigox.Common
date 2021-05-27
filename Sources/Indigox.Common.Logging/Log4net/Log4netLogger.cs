using log4net;
using log4net.Config;
using System;
using System.IO;

namespace Indigox.Common.Logging.Log4net
{
    class Log4netLogger : ILogger
    {

        static Log4netLogger()
        {
            string path = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "log4net.config" );
            XmlConfigurator.ConfigureAndWatch( new System.IO.FileInfo( path ) );
        }

        #region ILogger Members

        public void Debug( LogEntry log )
        {
            ILog logger = LogManager.GetLogger( log.CallerType );
            logger.Debug( log );
        }

        public void Info( LogEntry log )
        {
            ILog logger = LogManager.GetLogger( log.CallerType );
            logger.Info( log );
        }

        public void Warn( LogEntry log )
        {
            ILog logger = LogManager.GetLogger( log.CallerType );
            logger.Warn( log );
        }

        public void Error( LogEntry log )
        {
            ILog logger = LogManager.GetLogger( log.CallerType );
            logger.Error( log );
        }

        public void Fatal( LogEntry log )
        {
            ILog logger = LogManager.GetLogger( log.CallerType );
            logger.Fatal( log );
        }

        #endregion
    }
}
