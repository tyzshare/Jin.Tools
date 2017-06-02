using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// Sql 通配符转义帮助类
    /// </summary>
    public static class WildcardEscapeHelper
    {
        /// <summary>
        /// 通配符转义
        /// </summary>
        /// <param name="key">指定字符串</param>
        /// <returns></returns>
        public static string WildcardEscape(string key)
        {
            string str = "";
            if (key.Contains("%"))
            {
                string value = @"\%";
                str = key.Replace("%", value);
            }
            else if (key.Contains("_"))
            {
                string value = @"\_";
                str = key.Replace("_", value);
            }
            else if (key.Contains(@"\"))
            {
                string value = @"\\";
                str = key.Replace(@"\", value);
            }
            else
            {
                str = key;
            }
            return str;
        }
    }
}
