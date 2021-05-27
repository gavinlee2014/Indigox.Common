using System;
using System.Collections.Generic;
using System.Reflection;
using Indigox.Common.Utilities.Exceptions;

namespace Indigox.Common.Utilities
{
    public static class EnumUtil
    {
        public static string GetDescription(object val)
        {
            string desc = "";

            if (val == null)
            {
                throw new ArgumentNotEnumValueException("val");
            }

            Type t = val.GetType();

            if (!t.IsEnum)
            {
                throw new ArgumentNotEnumValueException("val");
            }

            Dictionary<int, string> _values = GetEnumValueDescriptionDictionary(t);
            if (_values.ContainsKey((int)val))
            {
                desc = _values[(int)val];
            }

            return desc;
        }

        public static string GetDescription(Type t, int val)
        {
            string desc = "";

            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            if (!t.IsEnum)
            {
                throw new ArgumentNotEnumValueException("val");
            }

            Dictionary<int, string> _values = GetEnumValueDescriptionDictionary(t);
            if (_values.ContainsKey(val))
            {
                desc = _values[val];
            }

            return desc;
        }

        public static object GetValue(Type t, int val)
        {
            return EnumUtil.GetValue(t, val, false);
        }

        public static object GetValue(Type t, int val, bool check)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            if (!t.IsEnum)
            {
                throw new TypeNotEnumTypeException(t);
            }

            //Dictionary<int, object> _values = GetEnumValueDictionary(t);
            //if (_values.ContainsKey(val))
            //{
            //    return _values[val];
            //}
            //else
            //{
            //    throw new EnumValueNotFoundException(t, val);
            //}
            if (check)
            {
                if (!IsDefined(t, val))
                {
                    throw new EnumValueNotFoundException(t, val);
                }
            }
            return Enum.ToObject(t, val);
        }

        private static bool IsDefined(Type t, int val)
        {
            return Enum.IsDefined(t, val);
        }

        private static bool IsFlagEnumType(Type t)
        {
            object[] attrs = t.GetCustomAttributes(typeof(FlagsAttribute), true);
            return attrs.Length == 1;
        }

        private static Dictionary<int, object> GetEnumValueDictionary(Type t)
        {
            if (!_EnumValueDictionaryCache.ContainsKey(t))
            {
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
                Dictionary<int, object> _values = new Dictionary<int, object>(fields.Length);
                foreach (FieldInfo field in fields)
                {
                    object[] attrs = field.GetCustomAttributes(typeof(EnumValueDescriptionAttribute), false);
                    if (attrs.Length == 1)
                    {
                        _values.Add((int)field.GetValue(null), field.GetValue(null));
                    }
                }
                _EnumValueDictionaryCache.Add(t, _values);
            }
            return _EnumValueDictionaryCache[t];
        }

        private static Dictionary<int, string> GetEnumValueDescriptionDictionary(Type t)
        {
            if (!_EnumValueDescriptionDictionaryCache.ContainsKey(t))
            {
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
                Dictionary<int, string> _values = new Dictionary<int, string>(fields.Length);
                foreach (FieldInfo field in fields)
                {
                    object[] attrs = field.GetCustomAttributes(typeof(EnumValueDescriptionAttribute), false);
                    if (attrs.Length == 1)
                    {
                        _values.Add((int)field.GetValue(null), ((EnumValueDescriptionAttribute)attrs[0]).Description);
                    }
                }
                _EnumValueDescriptionDictionaryCache.Add(t, _values);
            }
            return _EnumValueDescriptionDictionaryCache[t];
        }

        private static Dictionary<Type, Dictionary<int, object>> _EnumValueDictionaryCache = new Dictionary<Type, Dictionary<int, object>>();
        private static Dictionary<Type, Dictionary<int, string>> _EnumValueDescriptionDictionaryCache = new Dictionary<Type, Dictionary<int, string>>();
    }

    public static class EnumUtil<T>
    {
        public static string GetDescription(T val)
        {
            return EnumUtil.GetDescription(val);
        }

        public static string GetDescription(int val)
        {
            return EnumUtil.GetDescription(GetValue(val));
        }

        public static T GetValue(int val)
        {
            return (T)EnumUtil.GetValue(typeof(T), val);
        }

        public static T GetValue(int val, bool check)
        {
            return (T)EnumUtil.GetValue(typeof(T), val, check);
        }
    }
}
