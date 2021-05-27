
namespace Indigox.Common.Logging
{
    interface ILogger
    {
        void Debug(LogEntry log);
        void Info(LogEntry log);
        void Warn(LogEntry log);
        void Error(LogEntry log);
        void Fatal(LogEntry log);
    }
}
