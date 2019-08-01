using System;
using System.Collections.Generic;
using System.Linq;


namespace KeMengUtils.DataHelper
{
    /// <summary>
    /// 递归整理父子级帮助类
    /// </summary>
    public static class SortData
    {
        /// <summary>
        /// 整理成父级在上，自己在下的数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="parents">父级对象</param>
        /// <param name="origin">清除已整理的对象</param>
        /// <param name="func">条件</param>
        /// <param name="do">执行整理操作</param>
        /// <returns></returns>
        public static List<T> SortToTree<T>(this List<T> parents, List<T> origin, Func<T, bool> func, Action @do)
        {

            return parents.IsNull() ? null : new Func<List<T>>(() =>
            {
                foreach (var item in parents)
                {
                    var result = origin.Where(func).ToList();
                    if (result.Count() > 0)
                    {
                        result.ForEach(a => origin.Remove(a));
                        result.SortToTree<T>(origin, func, @do);
                        @do.Invoke();
                    }
                }
                return parents;
            }).Invoke();
        }
    }
}
