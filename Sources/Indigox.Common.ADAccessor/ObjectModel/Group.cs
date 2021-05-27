
namespace Indigox.Common.ADAccessor.ObjectModel
{
    public class Group : Entry
    {
        public string Mail { get; set; }
        public string Account
        {
            get
            {
                return this.Mail.Substring(0, this.Mail.IndexOf("@"));
            }
            set
            {
            }
        }
    }
}
