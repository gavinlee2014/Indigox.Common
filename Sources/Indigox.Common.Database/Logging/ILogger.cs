
namespace Indigox.Common.Data.Logging
{
    interface ILogger
    {
        void Debug(object msg);

        void Info(object msg);

        void Error(object msg);
    }
}
