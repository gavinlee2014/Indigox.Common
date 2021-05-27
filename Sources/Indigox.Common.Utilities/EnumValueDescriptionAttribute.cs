using System;

namespace Indigox.Common.Utilities
{
    [global::System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumValueDescriptionAttribute : System.Attribute
    {

        public string Description { get; set; }

        public EnumValueDescriptionAttribute(string description)
        {
            this.Description = description;
        }
    }
}
