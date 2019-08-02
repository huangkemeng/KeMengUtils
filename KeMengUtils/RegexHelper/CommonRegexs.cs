﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace KeMengUtils.RegexHelper
{
    public class CommonRegexs
    {

        private static CommonRegexs _instance = null;

        private static readonly object lockO = new object();
        private CommonRegexs() { }

        public static CommonRegexs GetInstance()
        {
            if (_instance == null)
            {
                lock (lockO)
                {
                    if (_instance == null)
                    {
                        _instance = new CommonRegexs();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// 全部都是数字
        /// </summary>
        /// <returns></returns>
        public Regex OnlyNumber()
        {
            return new Regex(@"^[0-9]*$");
        }

        /// <summary>
        /// 全部都是数字，且字符个数为m
        /// </summary>
        /// <param name="m">字符个数</param>
        /// <returns></returns>
        public Regex OnlyMNumber(int m)
        {
            return new Regex(@"^\d{" + m + "}$");
        }

        /// <summary>
        /// 全部都是数字，且字符个数至少为m
        /// </summary>
        /// <param name="m">字符个数</param>
        /// <returns></returns>
        public Regex AtLeastMNumber(int m)
        {
            return new Regex(@"^\d{" + m + ",}$");
        }

        /// <summary>
        /// 全部都是数字，且字符个数为m-n
        /// </summary>
        /// <param name="m">最少的字符个数</param>
        /// <param name="n">最多的字符个数</param>
        /// <returns></returns>
        public Regex OnlyNumberBetweenMN(int m, int n)
        {
            if (m > n)
            {
                throw new ArgumentException("参数m的值不允许大于参数n的值");
            }
            return new Regex(@"^\d{" + m + "," + n + "}$");
        }
    }
}
