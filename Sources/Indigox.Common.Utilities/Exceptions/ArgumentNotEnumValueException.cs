using System;

namespace Indigox.Common.Utilities.Exceptions
{
    public class ArgumentNotEnumValueException : Exception
    {

        public ArgumentNotEnumValueException(string argumentName)
        {
            this.ArgumentName = argumentName;
        }

        public string ArgumentName { get; set; }

        public override string Message
        {
            get
            {
                return string.Format("参数 {0} 不允许非枚举类型的值。", this.ArgumentName);
            }
        }
    }
}
