using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 获取配置文件中的某些信息
    /// </summary>
    public static class GetConfigMsgHelper
    {
        public static string GetValueByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
