using System;
using System.Collections;
using Indigox.Common.Utilities;

namespace Indigox.Common.EventBus
{
    public class SingletoneFactory
    {
        public static T GetInstance<T>()
        {
            Type type = typeof(T);
            if (!instances.ContainsKey(type))
            {
                instances.Add(type, ReflectUtil.CreateInstance(type));
            }
            return (T)instances[type];
        }

        public static object GetInstance(Type type)
        {
            if (!instances.ContainsKey(type))
            {
                instances.Add(type, ReflectUtil.CreateInstance(type));
            }
            return instances[type];
        }

        public static object GetInstance(string typeName)
        {
            Type type = ReflectUtil.GetType(typeName);
            if (!instances.ContainsKey(type))
            {
                instances.Add(type, ReflectUtil.CreateInstance(type));
            }
            return instances[type];
        }


        /// <summary>
        /// useful for test
        /// </summary>
        public static void Clear()
        {
            instances.Clear();
        }

        private static Hashtable instances = new Hashtable();
    }
}
