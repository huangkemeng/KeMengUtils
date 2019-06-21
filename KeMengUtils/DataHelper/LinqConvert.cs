using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace KeMengUtils.DataHelper
{
    /// <summary>
    /// Linq拓展类
    /// </summary>
    public static class LinqConvert
    {
        /// <summary>
        /// 转换成JArray类型
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="list">List集合</param>
        /// <returns></returns>
        public static JArray ToJArray<T>(this List<T> list)
        {
            try
            {
                return list == null ? null : JArray.FromObject(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 转换成JObject类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static JObject ToJObject(this object obj)
        {
            try
            {
                return obj == null ? null : JObject.FromObject(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// <summary>
        /// 非空DataTable转换成Jarray
        /// </summary>
        /// <param name="dt">DataTable 表</param>
        /// <returns></returns>
        public static JArray ToJArray(this DataTable dt)
        {
            try
            {
                return dt == null || dt.Rows.Count == 0 ? null : JArray.FromObject(dt.Rows[0].Table);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
