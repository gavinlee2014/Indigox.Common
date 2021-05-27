using System;
using System.Collections.Generic;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Utilities;
using Indigox.Common.Logging;

namespace Indigox.Common.EventBus
{
    public static class EventTrigger
    {
        public static void Trigger(object source, IEvent e)
        {
            if (e == null)
                throw new ArgumentNullException("e");
            Log.Debug(String.Format("event fire:{0},source is {1}", e.GetType().Name, source.GetType().FullName));

            IList<EventRegItem> items = EventRegister.Instance.GetEventItem(e.GetType(), source.GetType());
            foreach (var item in items)
            {
                object[] args = new object[] { source, e };
                Log.Debug(String.Format("event fire:{0},invoke method:{1}.{2}()", 
                    e.GetType().Name, 
                    item.Listener.GetType().FullName,
                    item.MethodName));
                ReflectUtil.InvokeMethod(item.Listener, item.MethodName, EventArgTypes, args);
            }
        }

        static readonly Type[] EventArgTypes = new Type[] { typeof(object), typeof(IEvent) };
    }
}
