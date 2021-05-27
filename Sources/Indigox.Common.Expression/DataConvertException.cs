using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Expression
{
    public class DataConvertException : Exception
    {
        private Type originType;
        private Type destinationType;
        private string reason;

        public Type OriginType
        {
            get { return this.originType; }
        }

        public Type DestinationType
        {
            get { return this.destinationType; }
        }

        public string Reason
        {
            get { return reason; }
        }

        public DataConvertException( Type originType, Type destinationType )
            : base( BuildMessage( originType, destinationType, null ) )
        {
            this.originType = originType;
            this.destinationType = destinationType;
        }

        public DataConvertException( Type originType, Type destinationType, string reason )
            : base( BuildMessage( originType, destinationType, reason ) )
        {
            this.originType = originType;
            this.destinationType = destinationType;
            this.reason = reason;
        }

        public DataConvertException( Type originType, Type destinationType, Exception innerException )
            : base( BuildMessage( originType, destinationType, null ), innerException )
        {
            this.originType = originType;
            this.destinationType = destinationType;
        }

        private static string BuildMessage( Type originType, Type destinationType, string reason )
        {
            string msg = string.Format( "Can't convert from {0} to {1}", originType.Name, destinationType.Name );
            if ( !string.IsNullOrEmpty( reason ) )
            {
                msg += ", because " + reason;
            }
            return msg;
        }
    }
}
