using System;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;

namespace Indigox.Common.Logging.Log4net.Layout.Pattern
{
    class CategoryPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LogEntry log = loggingEvent.MessageObject as LogEntry;
            if (log == null)
            {
                throw new Exception();
            }

            if (log.Category == null)
            {
                return;
            }
            string[] category = log.Category as string[];

            writer.Write(string.Join(";", category));
        }
    }
}
