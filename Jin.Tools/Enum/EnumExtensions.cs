using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 获取枚举的描述[DescriptionAttribute],使用缓存，减少反射的次数，提高效率
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class EnumExtensions
    {
        #region caches

        static readonly ICache<Type, Dictionary<int, string>> _descriptionCache = CacheFactory.Instance.CreateMemoryCache<Type, Dictionary<int, string>>();

        static object _descriptionLock = new object();

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
    }
}
