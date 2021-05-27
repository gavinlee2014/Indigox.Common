using System;
using System.Collections.Generic;

namespace Indigox.Common.DomainModels.Repository
{
    public class InstanceTypeMapping
    {
        private static Dictionary<Type, Type> InstanceTypeMap;
        private volatile static InstanceTypeMapping _instance = null;
        private static readonly object lockHelper = new object();
        private InstanceTypeMapping()
        {
            InstanceTypeMap = new Dictionary<Type, Type>();
        }

        public static InstanceTypeMapping Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockHelper)
                    {
                        if (_instance == null)
                            _instance = new InstanceTypeMapping();
                    }
                }
                return _instance;
            }
        }

        static InstanceTypeMapping()
        {
            InstanceTypeMap = new Dictionary<Type, Type>();
        }

        public void registerInstance(Type interfaceType, Type instanceType)
        {
            if (InstanceTypeMap.ContainsKey(interfaceType))
            {
                InstanceTypeMap[interfaceType] = instanceType;
            }
            else
            {
                InstanceTypeMap.Add(interfaceType, instanceType);
            }
        }

        /// <summary>
        /// 获取映射类型，若没有映射类型返回当前类型
        /// </summary>
        /// <remarks>
        /// 在有多个实现类的情况下使用接口类进行Get方法查询时会出错，
        /// 使用此方法进行一次转换
        /// </remarks>
        /// <returns></returns>
        public static Type GetMappedClass<T>()
        {
            return GetMappedClass(typeof(T));
        }

        /// <summary>
        /// 获取映射类型，若没有映射类型返回当前类型
        /// </summary>
        /// <remarks>
        /// 在有多个实现类的情况下使用接口类进行Get方法查询时会出错，
        /// 使用此方法进行一次转换
        /// </remarks>
        /// <returns></returns>
        public static Type GetMappedClass(Type type)
        {
            AssertArgumentIsNotNull(type, "type");
            if (InstanceTypeMap.ContainsKey(type))
            {
                return InstanceTypeMap[type];
            }
            return type;
        }

        private static void AssertArgumentIsNotNull(object value, string argName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

    }
}
