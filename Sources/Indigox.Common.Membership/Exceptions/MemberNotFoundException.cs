using System;

namespace Indigox.Common.Membership.Exceptions
{
    public class MemberNotFoundException : Exception
    {
        public const string TYPE_ID = "ID";
        public const string TYPE_Account = "Account";

        public MemberNotFoundException( object key, string keyType )
        {
            this.key = key;
            this.keyType = keyType;
        }

        private object key;
        private string keyType;

        public string KeyType
        {
            get { return this.keyType; }
        }

        public object Key
        {
            get { return this.key; }
        }

        public override string Message
        {
            get
            {
                string strKey = ( ( this.Key == null ) ? "null" : this.Key.ToString() );
                return string.Format( "未找到成员：[{1}:{0}]。", strKey, this.keyType );
            }
        }
    }
}