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
        /// <param name="datetime"><see cref="DateTime"/>一个日期</param>
        /// <returns><see cref="DateTime"/>第一天的日期</returns>
        public static DateTime FirstDayOfMonth(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, 1);
        }
        /// <summary>
        /// 该日期的该月的最后一天
        /// </summary>
        /// <param name="datetime"><see cref="DateTime"/>一个日期</param>
        /// <returns><see cref="DateTime"/>最后一天的日期</returns>
        public static DateTime LastDayOfMonth(this DateTime datetime)
        {
            return datetime.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }
    }
}
