using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class EnumExtensions
    {
        #region caches

        static readonly ICache<Type, Dictionary<int, string>> _descriptionCache = CacheFactory.Instance.CreateMemoryCache<Type, Dictionary<int, string>>();

        static object _descriptionLock = new object();

        static readonly ICache<Type, Dictionary<int, string>> _enumDescriptionCache = CacheFactory.Instance.CreateMemoryCache<Type, Dictionary<int, string>>();

        static object _enumDescriptionLock = new object();

        #endregion

        /// <summary>
        /// 获取当前Enum的描述(DescriptionAttribute)
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstead">当枚举值没有定义DescriptionAttribute,是否使用枚举名代替,默认是使用</param>
        /// <returns>枚举的Description</returns>
        public static string GetDescription(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            Dictionary<int, string> cache = _descriptionCache.Get(type, () =>
            {
                Dictionary<int, string> values = new Dictionary<int, string>();
                foreach (Enum item in Enum.GetValues(type))
                {
                    string name = Enum.GetName(type, item);
                    FieldInfo field = type.GetField(name);
                    DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attribute == null)
                    {
                        if (!nameInstead)
                        {
                            name = null;
                        }
                    }
                    else
                    {
                        name = attribute.Description;
                    }
                    values.Add(item.GetHashCode(), name);
                }
                return values;
            });
            string description;
            lock (_descriptionLock)
            {
                cache.TryGetValue(value.GetHashCode(), out description);
            }
            return description;
        }

        /// <summary>
        /// 通过类型和枚举值返回对应的描述(EnumDescriptionAttribute)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value, Boolean nameInstead = true)
        {

            Type type = value.GetType();
            Dictionary<int, string> cache = _enumDescriptionCache.Get(type, () =>
            {
                Dictionary<int, string> values = new Dictionary<int, string>();
                foreach (Enum item in Enum.GetValues(type))
                {
                    string name = Enum.GetName(type, item);
                    FieldInfo field = type.GetField(name);
                    EnumDescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(EnumDescriptionAttribute)) as EnumDescriptionAttribute;
                    if (attribute == null)
                    {
                        if (!nameInstead)
                        {
                            name = null;
                        }
                    }
                    else
                    {
                        name = attribute.DefaultDescription;
                    }
                    values.Add(item.GetHashCode(), name);
                }
                return values;
            });
            string description;
            lock (_enumDescriptionLock)
            {
                cache.TryGetValue(value.GetHashCode(), out description);
            }
            return description;
        }
    }
}
