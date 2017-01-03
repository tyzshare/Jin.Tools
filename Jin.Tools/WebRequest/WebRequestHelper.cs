using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace System
{
    public static class WebRequestHelper
    {
        /// <summary>
        /// 请求Url地址，获取返回数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetResponse(string url)
        {
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.Default);
            var result = reader.ReadToEnd();
            return result;
        }
    }
}
