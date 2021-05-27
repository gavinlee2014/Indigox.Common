
namespace Indigox.Common.Serialization
{
    public class SerializerSettings
    {
        public SerializerSettings()
        {
            this.Indent = false;
            this.WriteType = true;
            this.WriteTypeAlias = true;
        }

        public bool Indent { get; set; }
        public bool WriteType { get; set; }
        public bool WriteTypeAlias { get; set; }
    }
}
