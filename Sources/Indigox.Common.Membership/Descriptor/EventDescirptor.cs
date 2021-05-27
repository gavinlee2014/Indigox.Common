using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Indigox.Common.EventBus.Interface.Event;

namespace Indigox.Common.Membership.Descriptor
{
    public class EventDescriptor
    {
        public static string GetEventDescript(IEvent e)
        {
            Type type = e.GetType();
            StringBuilder builder = new StringBuilder();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value=property.GetValue(e, null);
                if (value is IDictionary<string, object>)
                {
                    IDictionary<string, object> dic = value as IDictionary<string, object>;
                    foreach (string key in dic.Keys)
                    {
                        builder.Append(string.Format("{0}: {1},", key, dic[key]));
                    }
                }
                else
                {
                    builder.Append(string.Format("{0}: {1},", property.Name, value));
                }
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
    }
}
