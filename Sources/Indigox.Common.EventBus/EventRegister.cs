using System;
using System.Collections.Generic;

namespace Indigox.Common.EventBus
{
    public class EventRegister
    {
        private EventRegister()
        {
        }

        private List<EventRegItem> eventList = new List<EventRegItem>();

        private static EventRegister instance;

        public static EventRegister Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventRegister();
                }
                return instance;
            }
        }

        public void Register(Type eventType, Type eventSourceType, object listener, string handlerMethod)
        {
            EventRegItem item = new EventRegItem(eventType, eventSourceType, listener, handlerMethod);
            if (!this.eventList.Contains(item))
            {
                this.eventList.Add(item);
            }
        }

        public void Unregister(Type eventType, Type eventSourceType, object listener, string handlerMethod)
        {
            EventRegItem item = new EventRegItem(eventType, eventSourceType, listener, handlerMethod);
            if (this.eventList.Contains(item))
            {
                this.eventList.Remove(item);
            }
        }

        public IList<EventRegItem> GetEventItem(Type eventType, Type eventSourceType)
        {
            List<EventRegItem> result = new List<EventRegItem>();
            foreach (EventRegItem item in this.eventList)
            {

                if (item.EventType.Equals(eventType) && item.SourceType.IsAssignableFrom(eventSourceType))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
