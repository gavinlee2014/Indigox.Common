using System;

namespace Indigox.Common.EventBus
{
    public class EventRegItem
    {
        internal EventRegItem(Type eventType, Type sourceType, object listener, string methodName)
        {
            this.methodName = methodName;
            this.eventType = eventType;
            this.sourceType = sourceType;
            this.listener = listener;
        }

        private Type eventType;
        private Type sourceType;
        private object listener;
        private string methodName;

        public Type EventType
        {
            get { return this.eventType; }
        }

        public Type SourceType
        {
            get { return this.sourceType; }
        }

        public object Listener
        {
            get { return this.listener; }
        }

        public string MethodName
        {
            get { return this.methodName; }
        }

        public override bool Equals(object obj)
        {
            EventRegItem item = obj as EventRegItem;
            if (item == null)
            {
                throw new ArgumentException("Not the same type");
            }
            return (
                (((this.eventType == null) && (item.eventType == null)) || item.eventType.Equals(this.eventType))
                && (((this.sourceType == null) && (item.sourceType == null)) || item.sourceType.Equals(this.sourceType))
                && (((this.listener == null) && (item.listener == null)) || item.listener.Equals(this.listener))
                && ((String.IsNullOrEmpty(this.methodName) && String.IsNullOrEmpty(item.methodName)) || item.methodName.Equals(this.methodName))
                );
        }

        public override int GetHashCode()
        {
            int code = 31;
            if (this.eventType != null)
                code ^= this.eventType.GetHashCode();
            if (this.sourceType != null)
                code ^= this.sourceType.GetHashCode();
            if (this.listener != null)
                code ^= this.listener.GetHashCode();
            if (this.methodName != null)
                code ^= this.methodName.GetHashCode();
            return code;
        }
    }
}
