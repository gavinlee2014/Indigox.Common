using System;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;

namespace Indigox.Common.Logging.Log4net.Layout.Pattern
{
    class IdentityPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LogEntry log = loggingEvent.MessageObject as LogEntry;
            if (log == null)
            {
                throw new Exception();
            }

            writer.Write(log.Id);
        }
    }
}
