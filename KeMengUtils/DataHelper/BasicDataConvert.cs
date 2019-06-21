using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KeMengUtils.DataHelper
{
    /// <summary>
    /// 基础数据拓展类
    /// </summary>
    public static class BasicDataExtends
    {
        /// <summary>
        /// 字符串转换为日期时
        /// </summary>
        /// <param name="str">表示日期的字符串</param>
        /// <returns>转换后的日期，转换失败则返回日期1970/1/1</returns>
        public static DateTime ToDateTime(this string str)
        {
            return str.IsDateTime() ? Convert.ToDateTime(str) : new DateTime(1970, 1, 1);
        }

        /// <summary>
        /// 字符串是否全为空格或null
        /// </summary>
        /// <param name="str">用于判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsNullOrWhite(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 字符串是否不全为空格或null
        /// </summary>
        /// <param name="str">用于判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsNotNullOrWhite(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// string是否全部为（null或空格）
        /// </summary>
        /// <param name="str">用于判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool StringsIsNullOrWhite(params string[] str)
        {
            if (str == null)
            {
                return true;
            }
            foreach (var item in str)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 对象是否全部为null
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>判断结果</returns>
        public static bool ObjectsIsNull(params object[] obj)
        {
            if (obj == null)
            {
                return true;
            }
            foreach (var item in obj)
            {
                if (item != null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断对象是否为null
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>判断结果</returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        /// <summary>
        /// 判断对象是否不为null
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
        /// <summary>
        /// 字符串为空则返回的结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="ifnull_result">如果为空则返回的结果</param>
        /// <param name="ifnotnull_result">如果不为空则返回的结果</param>
        /// <returns></returns>
        public static T IsNullThen<T>(this string str, T ifnull_result, T ifnotnull_result)
        {
            return str.IsNullOrWhite() ? ifnull_result : ifnotnull_result;
        }
        /// <summary>
        /// 字符串为空则返回的字符串
        /// </summary>
        /// <param name="current_string">字符串</param>
        /// <param name="ifnull_string">如果为空则返回的字符串</param>
        /// <param name="ifnotnull_string">如果不为空则返回的字符串</param>
        /// <returns></returns>
        public static string IfNullThen(this string current_string, string ifnull_string, string ifnotnull_string = null)
        {
            return current_string.IsNullOrWhite() ? ifnull_string : ifnotnull_string ?? current_string;
        }

        /// <summary>
        /// 字符串不为空则返回的字符串
        /// </summary>
        /// <param name="current_string">字符串</param>
        /// <param name="ifnotnull_string">如果不为空则返回的字符串</param>
        /// <param name="ifnull_string">如果为空则返回的字符串</param>
        /// <returns></returns>
        public static string IfNotNullThen(this string current_string, string ifnotnull_string, string ifnull_string = null)
        {
            return !current_string.IsNullOrWhite() ? ifnotnull_string : ifnull_string ?? current_string;
        }
        /// <summary>
        /// 判断字符串是否为空并和其他字符串比较
        /// </summary>
        /// <param name="current_string">字符串</param>
        /// <param name="other_string">要比较字符串</param>
        /// <param name="ifnotnull_expression">如果字符串不为空，则返回啥</param>
        /// <returns></returns>
        public static bool IfNullThenEqualWith(this string current_string, string other_string, bool ifnotnull_expression = true)
        {
            return current_string.IsNullOrWhite() ? current_string == other_string : ifnotnull_expression;
        }
        /// <summary>
        /// 判断字符串是否不为空并和其他字符串比较
        /// </summary>
        /// <param name="current_string">字符串</param>
        /// <param name="other_string">要比较字符串</param>
        /// <param name="ifnull_expression">如果字符串为空，则返回啥</param>
        /// <returns></returns>
        public static bool IfNotNullThenEqualWith(this string current_string, string other_string, bool ifnull_expression = true)
        {
            return !current_string.IsNullOrWhite() ? current_string == other_string : ifnull_expression;
        }
        /// <summary>
        /// 判断字符串是否为空并返回其他判断
        /// </summary>
        /// <param name="current_string">字符串</param>
        /// <param name="ifnull_expression">当字符串为空进行时的判断</param>
        /// <param name="ifnotnull_expression">当字符串不为空进行时的判断</param>
        /// <returns></returns>
        public static bool IfNullThen(this string current_string, Func<bool> ifnull_expression, Func<bool> ifnotnull_expression)
        {
            return current_string.IsNullOrWhite() ? ifnull_expression.Invoke() : ifnotnull_expression.Invoke();
        }
        /// <summary>
        /// 判断字符串是否不为空并返回其他判断
        /// </summary>
        /// <param name="current_string">字符串</param>
        /// <param name="ifnotnull_expression">当字符串不为空进行时的判断</param>
        /// <param name="ifnull_expression">当字符串为空进行时的判断</param>
        /// <returns></returns>
        public static bool IfNotNullThen(this string current_string, Func<bool> ifnotnull_expression, Func<bool> ifnull_expression)
        {
            return !current_string.IsNullOrWhite() ? ifnotnull_expression.Invoke() : ifnull_expression.Invoke();
        }

        /// <summary>
        /// 判断是否是日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string str)
        {
            return DateTime.TryParse(str, out DateTime dt);
        }
        /// <summary>
        /// 判断是否是10进制整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt32(this string str)
        {
            try
            {
                Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 判断数据集是否为空或集合个数为0
        /// </summary>
        /// <param name="current_strings"></param>
        /// <returns></returns>
        public static bool IsNull(this IEnumerable<string> current_strings)
        {
            return current_strings == null || current_strings.Count() == 0;
        }
        /// <summary>
        /// 去前后空格升级版，空和null不会报错
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimUpgraded(this string str)
        {
            return str.IsNullOrWhite() ? str : str.Trim();
        }
        /// <summary>
        /// 去前后字符升级版，空和null不会报错
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <param name="char_of_trim">去除的字符</param>
        /// <returns></returns>
        public static string TrimUpgraded(this string str, char char_of_trim)
        {
            return str.IsNullOrWhite() ? str : str.Trim(char_of_trim);
        }
        /// <summary>
        /// 字符串转小写升级版，空和null不会报错
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToLowerUpgraded(this string str)
        {
            return str.IsNullOrWhite() ? str : str.ToLower();
        }
        /// <summary>
        /// 字符串转大写升级版，空和null不会报错
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpperUpgraded(this string str)
        {
            return str.IsNullOrWhite() ? str : str.ToUpper();
        }
        /// <summary>
        /// 获取当前字符串变量的变量名
        /// </summary>
        /// <param name="Expression">表达式</param>
        /// <returns></returns>
        private static string VariableName(Expression<Func<string>> @Expression)
        {
            MemberExpression expressionBody = (MemberExpression)@Expression.Body;
            return expressionBody.Member.Name;
        }

        /// <summary>
        /// 解码字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        /// <summary>
        /// 编码字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        /// <summary>
        /// 给url添加参数
        /// </summary>
        /// <returns></returns>
        public static string AddParam(this string url, Expression<Func<string>> @Expression)
        {
            if (url.IndexOf("1=1") == -1)
            {
                url = $"{url}?1=1";
            }
            string param_name = VariableName(@Expression);
            string compile_value = @Expression.Compile()?.Invoke();
            string param_value = compile_value.IsNullOrWhite() ? "" : compile_value.UrlEncode();
            string param_string = $"&{param_name}={param_value}";
            url = $"{url}{param_string}";
            return url;
        }

        /// <summary>
        /// string 转 decimal,转换失败则返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object str)
        {
            try
            {
                return Convert.ToDecimal(str);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 转成String,转换失败则返回null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(this object obj)
        {
            try
            {
                return Convert.ToString(obj);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为的10进制数，转换失败则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt32(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 转成Double类型，转换失败返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(this object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return 0;
            }
        }
    }
}
