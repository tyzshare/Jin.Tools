using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jin.Tools.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //var result = RedisHelper.redisClient.Get<string>("name");
            Type result = (Type)4;
        }
    }

    public enum Type
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 日常
        /// </summary>
        Daily = 1,
        /// <summary>
        /// 活动
        /// </summary>
        Activity = 2,
    }
}
