using System;

namespace Indigox.Common.Utilities.Exceptions
{
    public class EnumValueNotFoundException : Exception
    {
        public int Value { get; set; }

        public Type EnumType { get; set; }

        public EnumValueNotFoundException(Type t, int val)
        {
            this.EnumType = t;
            this.Value = val;
        }

        public override string Message
        {
            get
            {
                return string.Format("枚举类型 {0} 中没有找到值为 {1} 的枚举值。", this.EnumType.FullName, this.Value);
            }
        }
    }
}
