using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jin.Tools.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDataTime()
        {
            var date= DateTime.Now.ToyyyyMMddHHmmss();
            var day = DateTime.Now.DayOfWeek.GetDayOfWeekString();
        }
        [TestMethod]
        public void TestEnum()
        {
            //var genderCode = EnumTaskState.Enabled.GetHashCode();
            //var descri = EnumDescriptionAttribute.GetDescription(typeof(EnumTaskState), EnumTaskState.Enabled);

            //var descri1 = EnumExtensions.GetEnumDescription(EnumTaskState.Enabled);
            //var descri2 = EnumExtensions.GetDescription(EnumGender.Man);

            var descri3 = EnumGender.Man.GetDescription();
        }

        [TestMethod]
        public void TestHtmlHelper()
        {
            var str = @"<p style='color:#494949;font-family:&quot;font-size:15px;background-color:#FFFFFF;'><iframe id = 'iframe_0.299626863407366' src = 'data:text/html;charset=utf8,%3Cstyle%3Ebody%7Bmargin:0;padding:0%7D%3C/style%3E%3Cimg%20id=%22img%22%20src=%22https://dn-coding-net-production-pp.qbox.me/b5c54e5a-3ff6-4b70-afc6-2f86baec3a69.png?_=5575223%22%20style=%22border:none;max-width:1572px%22%3E%3Cscript%3Ewindow.onload%20=%20function%20()%20%7Bvar%20img%20=%20document.getElementById('img');%20window.parent.postMessage(%7BiframeId:'iframe_0.299626863407366',width:img.width,height:img.height%7D,%20'http://www.cnblogs.com');%7D%3C/script%3E' frameborder = '0' style = 'width:1572px;' ></iframe></p>";
            var result = HtmlHelper.RemoveTag(str, "iframe");

            //var str = "abc<span style=''>ccccc</span>bb<span>1234</span>";
            //var result = HtmlHelper.RemoveTag(str,"span");

        }
    }
}
