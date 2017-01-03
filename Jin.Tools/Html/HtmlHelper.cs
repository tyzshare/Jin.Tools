using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace System
{
    public class HtmlHelper
    {
        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="textToEncode">字符串</param>
        /// <returns>编码结果</returns>
        public static string Encode(string textToEncode)
        {
            if (string.IsNullOrEmpty(textToEncode))
                return string.Empty;

            return HttpUtility.HtmlEncode(textToEncode);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="textToDecode">字符串</param>
        /// <returns>解码结果</returns>
        public static string Decode(string textToDecode)
        {
            if (string.IsNullOrEmpty(textToDecode))
                return string.Empty;

            return HttpUtility.HtmlDecode(Regex.Replace(textToDecode, "&nbsp;", " ", RegexOptions.IgnoreCase));
        }

        /// <summary>
        /// 过滤HTML和SQL
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns></returns>
        public static string ConvertKeywords(object text)
        {
            return ConvertToSafeSQL(ConvertToSafeHtml(text.ToString()));
        }

        /// <summary>
        /// 过滤HTML和SQL
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns></returns>
        public static string ConvertOriginalKeywords(object text)
        {
            return ConvertToOriginalHtml(ConvertToOriginalSql(text.ToString()));
        }

        /// <summary>
        /// 转换危险html标识字符
        /// 如：\n,&等符号
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <returns></returns>
        public static string ConvertToSafeHtml(string text)
        {
            //replace &, <, >, and line breaks with <br />
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Trim();
                text = text.Replace("&", "&amp;");
                text = text.Replace("<", "&lt;");
                text = text.Replace("\"", "&quot;");
                text = text.Replace("'", "&#039;");
                text = text.Replace(">", "&gt;");
                text = text.Replace("\r", string.Empty);
                text = text.Replace("\n", "<br />");
              
            }
            return text;
        }
        public static string unConvertToSafeHtml(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Trim();
                text = text.Replace("&amp;", "&");
                text = text.Replace("&lt;", "<");
                text = text.Replace("&quot;", "\"");
                text = text.Replace("&#039;", "'");
                text = text.Replace("&gt;", ">");
                text = text.Replace("<br />", "\n");
                text = text.Replace("&nbsp;", " ");

            }
            return text;
        }

        /// <summary>
        /// 转换危险SQL标识字符
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <returns></returns>
        public static string ConvertToSafeSQL(string text)
        {
            //删除与数据库相关的词
            text = Regex.Replace(text, "where", "wh&#101;re", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "select", "sel&#101;ct", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "insert", "ins&#101;rt", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "create", "cr&#101;ate", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "delete", "del&#101;te", RegexOptions.IgnoreCase);
           // text = Regex.Replace(text, "count", "c&#111;unt", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "drop", "dro&#112", RegexOptions.IgnoreCase);
           // text = Regex.Replace(text, "truncate", "truncat&#101;", RegexOptions.IgnoreCase);
            //   text = Regex.Replace(text, "asc", "as&#99;", RegexOptions.IgnoreCase);
            //   text = Regex.Replace(text, "mid", "m&#105;d", RegexOptions.IgnoreCase);
            //    text = Regex.Replace(text, "char", "ch&#97;r", RegexOptions.IgnoreCase);
            //    text = Regex.Replace(text, "xp_cmdshell", "xp_cmdsh&#101;ll", RegexOptions.IgnoreCase);
            //      text = Regex.Replace(text, "exec", "ex&#101;c", RegexOptions.IgnoreCase);
            //     text = Regex.Replace(text, "administrators", "administr&#97;tors", RegexOptions.IgnoreCase);
            //   text = Regex.Replace(text, "and", "a&#110;d", RegexOptions.IgnoreCase);
            //     text = Regex.Replace(text, "user", "us&#101;r", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "update", "up&#100;ate", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "or", "o&#114;", RegexOptions.IgnoreCase);
            //      text = Regex.Replace(text, "net", "n&#101;t", RegexOptions.IgnoreCase);
            //text =  Regex.Replace(text,"*", "", RegexOptions.IgnoreCase);
            //text =  Regex.Replace(text,"-", "", RegexOptions.IgnoreCase);
           // text = Regex.Replace(text, "script", "s&#99;ript", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "alter", "alt&#101;r", RegexOptions.IgnoreCase);
            return text;
            

        }
        /// <summary>
        //　转换成过虑前的SQL字符串
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <returns></returns>
        public static string ConvertToOriginalSql(string text)
        {
            //删除与数据库相关的词
            /*
                 return str.Replace("<br />\r\n", "\r\n").Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace(" ", "&nbsp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace(" ", "&nbsp;").Replace(" where ", " wh&#101;re ").Replace(" select ", " sel&#101;ct ").Replace(" insert ", " ins&#101;rt ").Replace(" create ", " cr&#101;ate ").Replace(" drop ", " dro&#112 ").Replace(" alter ", " alt&#101;r ").Replace(" delete ", " del&#101;te ").Replace(" update ", " up&#100;ate ").Replace(" or ", " o&#114; ").Replace("\"", "&#34;").Replace("\r\n", "<br />\r\n");
             */
            text = Regex.Replace(text, "wh&#101;re", "where", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "sel&#101;ct", "select", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "ins&#101;rt", "insert", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "cr&#101;ate", "create", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "del&#101;te", "delete", RegexOptions.IgnoreCase);
           // text = Regex.Replace(text, "c&#111;unt", "count", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "dro&#112", "drop", RegexOptions.IgnoreCase);
            //text = Regex.Replace(text, "truncat&#101;", "truncate", RegexOptions.IgnoreCase);
            //text = Regex.Replace(text, "asc", "as&#99;", RegexOptions.IgnoreCase);
            //text = Regex.Replace(text, "m&#105;d", "mid", RegexOptions.IgnoreCase);
            //text = Regex.Replace(text, "ch&#97;r", "char", RegexOptions.IgnoreCase);
            //text = Regex.Replace(text, "xp_cmdsh&#101;ll", "xp_cmdshell", RegexOptions.IgnoreCase);
            //text = Regex.Replace(text, "ex&#101;c", "exec", RegexOptions.IgnoreCase);
            //text = Regex.Replace(text, "administr&#97;tors", "administrators", RegexOptions.IgnoreCase);
            //text = Regex.Replace(text, "a&#110;d", "and", RegexOptions.IgnoreCase);
            //text = Regex.Replace(text, "us&#101;r", "user", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "up&#100;ate", "update", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "o&#114;", "or", RegexOptions.IgnoreCase);
          //  text = Regex.Replace(text, "n&#101;t", "net", RegexOptions.IgnoreCase);
            //text =  Regex.Replace(text,"*", "", RegexOptions.IgnoreCase);
            //text =  Regex.Replace(text,"-", "", RegexOptions.IgnoreCase);
          //  text = Regex.Replace(text, "s&#99;ript", "script", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "alt&#101;r", "alter", RegexOptions.IgnoreCase);

            return text;


        }
     
        /// <summary>
        /// 还原危险html标识字符
        /// 如&amp;等
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <returns></returns>
        public static string ConvertToOriginalHtml(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("&amp;", "&");
                text = text.Replace("&lt;", "<");
                text = text.Replace("&quot;", "\"");
                text = text.Replace("&#039;", "'");
                text = text.Replace("&gt;", ">");
                text = text.Replace("<br />", "\n");
               
            }
            return text;
        }

        /// <summary>
        /// 去除Html标签
        /// </summary>
        /// <param name="text">待转化的字符串</param>
        /// <returns>经过转化的字符串</returns>
        public static string StripHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            Regex regExp = new Regex("<[\\s\\S]*?>", RegexOptions.IgnoreCase);
            string output = regExp.Replace(text, "");
            return output;
        }

        /// <summary>
        /// 去除html标签和客户端脚本代码
        /// </summary>
        /// <param name="text">待转化的字符串</param>
        /// <returns>经过转化的字符串</returns>
        public static string StripAllTags(string text)
        {
            return StripHtml(StripScript(StripStyle(text)));
        }

        /// <summary>
        /// 去除客户端脚本代码
        /// </summary>
        /// <param name="text">待转化的字符串</param>
        /// <returns>经过转化的字符串</returns>
        public static string StripScript(string text)
        {
            Regex objRegExp = new Regex("<script[^>]*>((.|\n)*?)</script>", RegexOptions.IgnoreCase);
            string output = objRegExp.Replace(text, "");
            return output;
        }

        /// <summary>
        /// 去除CSS样式代码
        /// </summary>
        /// <param name="text">待转化的字符串</param>
        /// <returns>经过转化的字符串</returns>
        public static string StripStyle(string text)
        {
            Regex objRegExp = new Regex("<style[^>]*>((.|\n)*?)</style>", RegexOptions.IgnoreCase);
            string output = objRegExp.Replace(text, "");
            return output;
        }

        /// <summary>
        /// 清理HTMLL标签
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearHtmlTag(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";

            return Regex.Replace(str, @"(<[a-zA-Z]+[^>]*>)|(</[a-zA-Z\d]+>)|(<!--[^~]*-->)|(&nbsp;)|(&gt)", "").Replace("<", "").Replace(">", "");
        }

        /// <summary>
        /// 将Unicode字符转为HTML形式输出(可对韩文进行转换)
        /// </summary>
        /// <param name="bstr"></param>
        /// <returns></returns>
        public static string UnicodeToHtml(string bstr)
        {
            if (string.IsNullOrEmpty(bstr))
                return "";

            byte[] bytes = Encoding.UTF8.GetBytes(bstr);
            char[] chars = Encoding.UTF8.GetChars(bytes);
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (char ch in chars)
            {
                num = ch;
                if ((num > 0xabff) && (num < 0xd7a5))
                {
                    builder.Append("&#" + num + ";");
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Appends a CSS class to a control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="newClass">The new class.</param>
        public static void AppendCssClass(WebControl control, string newClass)
        {
            if (control == null)
                throw new ArgumentNullException("Cannot add a css class to a null control");

            if (newClass == null)
                throw new ArgumentNullException("Cannot add a null css class to a control");

            string existingClasses = control.CssClass;
            if (String.IsNullOrEmpty(existingClasses))
            {
                control.CssClass = newClass;
                return;
            }

            string[] classes = existingClasses.Split(' ');
            foreach (string attributeValue in classes)
            {
                if (String.Equals(attributeValue, newClass, StringComparison.Ordinal))
                {
                    //value's already in there.
                    return;
                }
            }
            control.CssClass += " " + newClass;
        }

        /// <summary>
        /// Removes a CSS class to a control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="classToRemove">The new class.</param>
        public static void RemoveCssClass(WebControl control, string classToRemove)
        {
            if (control == null)
                throw new ArgumentNullException("Cannot remove a css class from a null control");

            if (classToRemove == null)
                throw new ArgumentNullException("Cannot remove a null css class from a control");

            string existingClasses = control.CssClass;
            if (String.IsNullOrEmpty(existingClasses))
                return; //nothing to remove

            string[] classes = existingClasses.Split(new string[] { " ", "\t", "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            string newClasses = string.Empty;
            foreach (string cssClass in classes)
            {
                if (!String.Equals(cssClass, classToRemove, StringComparison.Ordinal))
                {
                    newClasses += cssClass + " ";
                }
            }

            if (newClasses.EndsWith(" "))
                newClasses = newClasses.Substring(0, newClasses.Length - 1);
            control.CssClass = newClasses;
        }

        /// <summary>
        /// Appends the attribute value to the control appropriately.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AppendAttributeValue(WebControl control, string name, string value)
        {
            string existingValue = control.Attributes[name];
            if (String.IsNullOrEmpty(existingValue))
            {
                control.Attributes[name] = value;
                return;
            }
            else
            {
                string[] attributeValues = control.Attributes[name].Split(' ');
                foreach (string attributeValue in attributeValues)
                {
                    if (String.Equals(attributeValue, value, StringComparison.Ordinal))
                    {
                        //value's already in there.
                        return;
                    }
                }
                control.Attributes[name] += " " + value;
            }
        }
        /// <summary>
        /// 判断指定html内容中是否存在指定标记名元素
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static bool CheckTagExist(string html, string tagName)
        {
            Regex objRegExp = new Regex(string.Format("<{0}\\s+.*?[^><]*?>", tagName), RegexOptions.IgnoreCase);
            return objRegExp.IsMatch(html);
        }

        /// <summary>
        /// 替换URL地址为A标签HTML(注：跳过已经在A标签HTML内部的URL地址)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReplaceHtmlLink(string text)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            Match list = Regex.Match(text, @"(<a.*?>.*?</a>)|(http://[a-zA-Z0-9\.-]*\.[a-zA-Z0-9]{2,4}[a-zA-Z0-9\.!@#$%&&amp;*-+=/?\\]*)", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if (list == null || !list.Success)
                return text;

            int index = 0;
            string strSub = "";
            string str = text.Substring(0, list.Index);
            while (list != null && list.Success)
            {
                sb.Append(str);
                strSub += str;
                if (list.Groups[2] != null && !string.IsNullOrEmpty(list.Groups[2].Value))
                {
                    strSub += list.Groups[2].Value;
                    str = string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", list.Groups[2].Value);
                }
                else
                {
                    strSub += list.Groups[1].Value;
                    str = list.Groups[1].Value;
                }
                sb.Append(str);
                list = list.NextMatch();
                if (list.Success)
                {
                    index = list.Index;
                    str = text.Substring(strSub.Length, index - strSub.Length);
                }
            }
            sb.Append(text.Substring(strSub.Length));
            return sb.ToString();
        }
    }

}
