using System;

namespace KeMengUtils.DataHelper
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 该日期的该月的第一天
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static DateTime FirstDay(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, 1);
        }
        /// <summary>
        /// 该日期的该月的最后一天
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static DateTime LastDay(this DateTime datetime)
        {
            return datetime.FirstDay().AddMonths(1).AddDays(-1);
        }
    }
}
