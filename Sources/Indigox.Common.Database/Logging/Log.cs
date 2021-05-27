
namespace Indigox.Common.Data.Logging
{
    internal class Log
    {
        private static ILogger logger = new IndigoLogger();

        public static void Debug(object msg)
        {
            logger.Debug(msg);
        }

        public static void Info(object msg)
        {
            logger.Info(msg);
        }

        public static void Error(object msg)
        {
            logger.Error(msg);
        }
    }
}
