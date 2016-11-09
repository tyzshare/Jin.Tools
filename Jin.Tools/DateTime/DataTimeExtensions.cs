using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class DataTimeExtensions
    {
        public static string ToyyyyMMdd(this DateTime o)
        {
            return o.ToString("yyyy-MM-dd");
        }
        public static string ToyyyyMMddHHmmss(this DateTime o)
        {
            return o.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string ToyyyyMMddHHmm(this DateTime o)
        {
            return o.ToString("yyyy-MM-dd HH:mm");
        }
     

        public static string GetDayOfWeekString(this DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "星期日";                    
                case DayOfWeek.Monday:
                    return "星期一";
                case DayOfWeek.Tuesday:
                    return "星期二";
                case DayOfWeek.Wednesday:
                    return "星期三";
                case DayOfWeek.Thursday:
                    return "星期四";
                case DayOfWeek.Friday:
                    return "星期五";
                case DayOfWeek.Saturday:
                    return "星期六";
                default:
                    return "";
            }
        }

    }
}
