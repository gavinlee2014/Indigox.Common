using System;
using System.Runtime.Serialization;

namespace Indigox.Common.Exceptions
{
    public class RuntimeException : Exception
    {
        public RuntimeException()
            : base()
        {
        }

        public RuntimeException(string message)
            : base(message)
        {
        }

        public RuntimeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public RuntimeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
