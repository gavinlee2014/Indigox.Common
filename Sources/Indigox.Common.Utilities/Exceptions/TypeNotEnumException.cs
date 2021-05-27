using System;

namespace Indigox.Common.Utilities.Exceptions
{
    public class TypeNotEnumTypeException : Exception
    {
        public Type Type { get; set; }

        public TypeNotEnumTypeException(Type t)
        {
            this.Type = t;
        }

        public override string Message
        {
            get
            {
                return string.Format("{0} 不是枚举类型", this.Type.FullName);
            }
        }
    }
}
