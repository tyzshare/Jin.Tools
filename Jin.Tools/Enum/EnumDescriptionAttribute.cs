using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 【已弃用】，枚举备注以后使用系统的[DescriptionAttribute]方式，并使用EnumExtensions里面获取枚举的描述方法
    /// </summary>
    [Obsolete("以后使用系统的[DescriptionAttribute]方式，并使用EnumExtensions里面获取枚举的描述方法")]
    public class EnumDescriptionAttribute : Attribute
    {
        public string DefaultDescription { get; set; }


        /// <summary>
        /// 通过枚举类型和枚举返回对应的描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="enumValue">枚举</param>
        /// <returns></returns>
        public static string GetDescription(Type type, object enumValue)
        {
            object[] objs = type.GetField(enumValue.ToString()).GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (objs.Length > 0)
            {
                return (objs[0] as EnumDescriptionAttribute).DefaultDescription;
            }
            return enumValue.ToString();
        }

    }
}
