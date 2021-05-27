using System;
using log4net.Layout.Pattern;

namespace Indigox.Common.Logging.Log4net.Layout.Pattern
{
    class DateTimePatternConverter : PatternLayoutConverter
    {
        protected override void Convert( System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent )
        {
            writer.Write( DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss.fff" ) );
        }
    }
}
