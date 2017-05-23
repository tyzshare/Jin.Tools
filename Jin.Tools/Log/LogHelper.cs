using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class LogHelper
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常</param>
        public static void Log(string message, Exception ex)
        {
            var logger = log4net.LogManager.GetLogger("log");
            logger.Info(message, ex);
        }
    }
}
