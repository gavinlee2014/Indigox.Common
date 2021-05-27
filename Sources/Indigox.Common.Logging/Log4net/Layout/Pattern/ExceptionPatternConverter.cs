using System;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;

namespace Indigox.Common.Logging.Log4net.Layout.Pattern
{
    class ExceptionPatternConverter : PatternLayoutConverter 
    {
        public ExceptionPatternConverter()
        {
            this.IgnoresException = false;
        }

        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LogEntry log = loggingEvent.MessageObject as LogEntry;
            if (log == null)
            {
                throw new Exception();
            }
            if (log.Exception == null)
            {
                return;
            }
            writer.Write(log.Exception.ToString());
        }
    }
}
