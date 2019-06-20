using System;
using System.Collections.Generic;
using System.Text;

namespace KeMengUtils.StringHelper
{
    public static class StringConvert
    {
        /// <summary>
        /// 转换成32位10进制int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToInt32(this string str)
        {
            return Convert.ToInt32(str);
        }
    }
}
