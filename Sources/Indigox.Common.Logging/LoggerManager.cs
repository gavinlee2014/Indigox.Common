using Indigox.Common.Logging.Log4net;

namespace Indigox.Common.Logging
{
    class LoggerManager
    {
        public static ILogger GetLogger()
        {
            return new Log4netLogger();
        }
    }
}
