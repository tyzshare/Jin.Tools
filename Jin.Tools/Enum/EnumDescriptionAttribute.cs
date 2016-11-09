using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  System
{
    public class EnumDescriptionAttribute : Attribute
    {
        public string DefaultDescription { get; set; }

     
        /// <summary>
        /// 通过类型和枚举值返回对应的描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(Type type, object enumValue)
        {
            object[] objs = type.GetField(enumValue.ToString()).GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if(objs.Length>0)
            {
                return (objs[0] as EnumDescriptionAttribute).DefaultDescription;
            }
            return enumValue.ToString();
        }

    }

}
