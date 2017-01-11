using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public  static class Md5Helper
    {
        /// <summary>
        /// 给字符串进行MD5加密运算，并放回加密后的字符串
        /// </summary>
        /// <param name="sDataIn"></param>
        /// <returns></returns>
        public static string GetMD5(string sDataIn)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }
        /// <summary>
        /// 比较Md5字符串是否相等
        /// </summary>
        /// <param name="inputPwd">用户输入的密码字符串</param>
        /// <param name="dataBasePwd">数据库中的加密字符串</param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(string inputPwd, string dataBasePwd)
        {
            if (GetMD5(inputPwd) == dataBasePwd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
