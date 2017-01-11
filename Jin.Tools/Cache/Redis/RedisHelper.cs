using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class RedisHelper
    {
        private static string host = GetConfigMsgHelper.GetValueByKey("host");
        private static string port = GetConfigMsgHelper.GetValueByKey("port");
        private static string pwd = GetConfigMsgHelper.GetValueByKey("pwd");
        private static string db = GetConfigMsgHelper.GetValueByKey("db");

        public static RedisClient redisClient;

        static RedisHelper()
        {
            redisClient = new RedisClient(host, Convert.ToInt32(port), pwd, Convert.ToInt64(db));
        }
    }

}
