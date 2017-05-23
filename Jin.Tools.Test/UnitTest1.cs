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
    }
}
