﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace KeMengUtils.DataHelper
{
    /// <summary>
    /// DataTable帮助类
    /// </summary>
    public static class DTHelper
    {
        private static string tempDbName = string.Empty;

        private static Dictionary<string, string> _MappedDic = null;

        private static IEnumerable<string> _MappingSourceColumns = null;

        private static IEnumerable<string> _MappingTargetColumns = null;

        /// <summary>
        /// 以可枚举的方式返回列集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<DataColumn> ColumnsAsEnumerable(this DataTable dt)
        {
            foreach (DataColumn dc in dt.Columns)
            {
                yield return dc;
            }
        }

        /// <summary>
        /// 用于创建临时表时的，生成一个由原表映射得到的列名的临时表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string CreateSqlColumnsString(this DataTable dt)
        {
            return string.Join(",", dt.ColumnsAsEnumerable().Where(a => { var name = dt.GetTargetColumn(a.ColumnName); return !name.IsNullOrWhite(); }).Select(a => { var name = dt.GetTargetColumn(a.ColumnName); return $"[{name}] {a.GetSqlDataType()}"; }));
        }

        /// <summary>
        /// 用于创建临时表时，生成一个跟原DataTable列名一模一样的临时表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string CreateSqlBySourceTb(this DataTable dt)
        {
            return string.Join(",", dt.ColumnsAsEnumerable().Select(a => $"[{a.ColumnName}] {a.GetSqlDataType()}"));
        }



        /// <summary>
        /// 判断表格是否包含某些列名，返回不包含的列名
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static IEnumerable<string> NotContainColumns(this DataTable dt, params string[] cols)
        {
            if (dt == null)
            {
                throw new ArgumentNullException(nameof(dt));
            }
            if (cols == null)
            {
                throw new ArgumentNullException(nameof(cols));
            }
            foreach (string col in cols)
            {
                if (!dt.Columns.Contains(col))
                {
                    yield return col;
                }
            }
        }

        /// <summary>
        /// 根据列名集合获取列集合
        /// </summary>
        /// <param name="col_names"></param>
        /// <returns></returns>
        public static IEnumerable<DataColumn> GetColumns(this string[] col_names)
        {
            foreach (string col_name in col_names)
            {
                yield return new DataColumn(col_name);
            }
        }

        /// <summary>
        /// 根据列名集合创建多个列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="col_names">列名集合</param>
        public static void CreateColumns(this DataTable dt, params string[] col_names)
        {
            foreach (string col_name in col_names)
            {
                dt.Columns.Add(col_name);
            }
        }

        /// <summary>
        /// 将某列的数据类型对应SqlServer中的数据类型
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static string GetSqlDataType(this DataColumn dc)
        {
            string result = string.Empty;
            switch (dc.DataType.ToString().ToLower())
            {
                case "system.string":
                    result = "nvarchar(512)";
                    break;
                case "system.int16":
                case "system.int32":
                    result = "int";
                    break;
                case "system.int64":
                    result = "float";
                    break;
                case "system.decimal":
                case "system.double":
                    result = "decimal(18,4)";
                    break;
                case "system.datetime":
                    result = "datetime";
                    break;
                case "system.byte[]":
                    result = "binary";
                    break;
                case "system.boolean":
                    result = "bit";
                    break;
                case "system.guid":
                    result = "uniqueidentifier";
                    break;
                case "object":
                    result = "variant";
                    break;
                default:
                    result = "nvarchar(512)";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 将SqlServer中的数据类型映射csharp中的数据类型名
        /// </summary>
        /// <param name="dtype"></param>
        /// <returns></returns>
        public static string GetCSharpDataType(this SqlDbType dtype)
        {
            string cSharpType = string.Empty;
            switch (dtype.ToString().ToLower())
            {
                case "bit":
                    cSharpType = "bool";
                    break;
                case "tinyint":
                    cSharpType = "byte";
                    break;
                case "smallint":
                    cSharpType = "short";
                    break;
                case "int":
                    cSharpType = "int";
                    break;
                case "bigint":
                    cSharpType = "long";
                    break;
                case "real":
                    cSharpType = "float";
                    break;
                case "float":
                    cSharpType = "double";
                    break;
                case "smallmoney":
                case "money":
                case "decimal":
                case "numeric":
                    cSharpType = "decimal";
                    break;
                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    cSharpType = "string";
                    break;
                case "samlltime":
                case "date":
                case "smalldatetime":
                case "datetime":
                case "datetime2":
                case "datetimeoffset":
                    cSharpType = "System.DateTime";
                    break;
                case "timestamp":
                case "image":
                case "binary":
                case "varbinary":
                    cSharpType = "byte[]";
                    break;
                case "uniqueidentifier":
                    cSharpType = "System.Guid";
                    break;
                case "variant":
                case "sql_variant":
                    cSharpType = "object";
                    break;
                default:
                    cSharpType = "string";
                    break;
            }
            return cSharpType;
        }

        /// <summary>
        /// 设置临时表名
        /// </summary>
        /// <param name="dt">临时表名</param>
        /// <param name="table_name">表名</param>
        /// <returns></returns>
        public static void SetTempDbName(this DataTable dt, string table_name = null)
        {
            if (!string.IsNullOrWhiteSpace(table_name))
            {
                tempDbName = table_name;
            }
            else
            {
                tempDbName = $"tb_{Guid.NewGuid().ToString().Replace("-", "").ToLower()}";
            }
        }

        /// <summary>
        /// 读取临时表名
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTempDbName(this DataTable dt)
        {
            return tempDbName;
        }

        /// <summary>
        /// 新增一行数据
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="dic">键值对</param>
        /// <param name="clone_dr">复制的行,不复制则填null</param>
        public static void AddNewRow(this DataTable dt, DataRow clone_dr = null, params KeyValuePair<string, object>[] dic)
        {
            if (dt == null)
            {
                throw new ArgumentNullException(nameof(dt));
            }
            DataRow dr = dt.NewRow();
            if (clone_dr != null)
            {
                dr.ItemArray = (object[])clone_dr.ItemArray.Clone();
            }
            if (dic != null && dic.Length > 0)
            {
                foreach (KeyValuePair<string, object> row in dic)
                {
                    dr[row.Key] = row.Value;
                }
            }
            dt.Rows.Add(dr);
        }

        /// <summary>
        /// 新增多行数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dics">数据字典</param>
        public static void AddManyRows(this DataTable dt, params Dictionary<string, object>[] dics)
        {
            if (dt == null)
            {
                throw new ArgumentNullException(nameof(dt));
            }
            if (dics == null)
            {
                throw new ArgumentNullException(nameof(dics));
            }
            foreach (Dictionary<string, object> dic in dics)
            {
                DataRow dr = dt.NewRow();
                foreach (KeyValuePair<string, object> row in dic)
                {
                    dr[row.Key] = row.Value;
                }
                dt.Rows.Add(dr);
            }
        }

        /// <summary>
        /// 添加多行数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rows">DataRow</param>
        public static void AddManyRows(this DataTable dt, params DataRow[] rows)
        {
            if (dt == null)
            {
                throw new ArgumentNullException(nameof(dt));
            }
            if (rows == null)
            {
                throw new ArgumentNullException(nameof(rows));
            }
            foreach (DataRow row in rows)
            {
                dt.Rows.Add(row);
            }
        }

        /// <summary>
        /// 创建一行数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static DataRow CreateNewRow(this DataTable dt, params KeyValuePair<string, object>[] dic)
        {
            if (dt == null)
            {
                throw new ArgumentNullException(nameof(dt));
            }

            if (dic == null)
            {
                throw new ArgumentNullException(nameof(dic));
            }
            DataRow dr = dt.NewRow();
            foreach (KeyValuePair<string, object> row in dic)
            {
                dr[row.Key] = row.Value;
            }
            return dr;
        }

        /// <summary>
        /// 创建多行数据
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="dics">行</param>
        /// <returns></returns>
        public static IEnumerable<DataRow> CreateManyRows(this DataTable dt, params Dictionary<string, object>[] dics)
        {
            if (dt == null)
            {
                throw new ArgumentNullException(nameof(dt));
            }

            if (dics == null)
            {
                throw new ArgumentNullException(nameof(dics));
            }

            foreach (Dictionary<string, object> dic in dics)
            {
                DataRow dr = dt.NewRow();
                foreach (KeyValuePair<string, object> row in dic)
                {
                    dr[row.Key] = row.Value;
                }
                yield return dr;
            }
        }

        /// <summary>
        /// 判断是否为空表
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="callback">回调方法</param>
        /// <returns></returns>
        public static bool IsEmpty(this DataTable dt, Action callback = null)
        {
            bool is_empty = dt == null || dt.Rows.Count == 0;
            if (is_empty)
            {
                callback?.Invoke();
            }
            return is_empty;
        }

        /// <summary>
        /// 表字段映射到数据库字段
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dic"></param>
        public static void SetMapping(this DataTable dt, Dictionary<string, string> dic)
        {
            _MappedDic = dic;
            _MappingSourceColumns = dic.Keys;
            _MappingTargetColumns = dic.Values;
        }

        /// <summary>
        /// 表字段和数据库字段的对应关系集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Dictionary<string, string> MappingDic(this DataTable dt)
        {
            return _MappedDic;
        }

        /// <summary>
        /// 获取表字段集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetMappingSourceColumns(this DataTable dt)
        {
            return _MappingSourceColumns;
        }

        /// <summary>
        /// 获取数据库字段集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetMappingTargetColumns(this DataTable dt)
        {
            return _MappingTargetColumns;
        }

        /// <summary>
        /// 通过数据库字段获取表字段名字
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="field_name">数据库字段</param>
        /// <returns></returns>
        public static string GetSourceColumnName(this DataTable dt, string field_name)
        {
            if (_MappedDic == null)
            {
                throw new InvalidOperationException("调用此方法之前请先调用SetMapping方法设置表头到数据库字段的映射");
            }
            if (dt == null)
            {
                throw new ArgumentNullException(nameof(dt));
            }
            if (string.IsNullOrWhiteSpace(field_name))
            {
                throw new ArgumentException("数据库表字段名不能为空或null", nameof(field_name));
            }
            if (!_MappedDic.Any(a => a.Value == field_name))
            {
                throw new KeyNotFoundException($"字典中未找到value为{field_name}的key");
            }
            return _MappedDic.Where(a => a.Value == field_name).FirstOrDefault().Key;
        }

        /// <summary>
        /// 通过表字段获取数据库字段
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="key">表字段</param>
        /// <returns></returns>
        public static string GetTargetColumn(this DataTable dt, string key)
        {
            if (_MappedDic == null)
            {
                throw new InvalidOperationException("调用此方法之前请先调用SetMapping方法设置表头到数据库字段的映射");
            }
            if (dt == null)
            {
                throw new ArgumentNullException(nameof(dt));
            }
            if (_MappedDic.TryGetValue(key, out string value))
            {
                return value;
            }
            throw new EvaluateException($"无法在设置的映射字典中通过表头名找到对应的数据库名");
        }

        /// <summary>
        /// 通过数据库字段名，获取表中的某行某列的值
        /// </summary>
        /// <param name="dr">行</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static object GetMappedValue(this DataRow dr, string value)
        {
            try
            {
                if (_MappedDic.Any(a => a.Value == value))
                {
                    string key = _MappedDic.Where(a => a.Value == value).FirstOrDefault().Key;
                    return dr[key];
                }
                throw new KeyNotFoundException($"Excel表头映射数据库字段的字典中不包含名为{value}的数据库字段");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool TryGetValue(this DataRow dr, string key, out object value)
        {

            try
            {
                value = dr[key];
                if (value == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        /// <summary>
        /// 直接返回原值
        /// </summary>
        /// <param name="dr">行</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        public static object GetValue(this DataRow dr, string key)
        {
            try
            {
                return dr[key];
            }
            catch (Exception ex)
            {
                throw new EvaluateException($"行中并不包含名为{key}的列", ex.GetInnermostException());
            }
        }

        /// <summary>
        /// 通过数据库字段名，设置原表的值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="db_field"></param>
        /// <param name="value"></param>
        public static void SetValueByMappedColumns<T>(this DataRow dr, string db_field, T value)
        {
            try
            {
                if (!_MappedDic.Any((a => a.Value == db_field)))
                {
                    throw new KeyNotFoundException($"映射的字典中并不包含名为{db_field}的Value");
                }
                string key = _MappedDic.Where(a => a.Value == db_field).FirstOrDefault().Key;
                dr[key] = value;
            }
            catch (Exception ex)
            {
                string key = _MappedDic.Where(a => a.Value == db_field).FirstOrDefault().Key;
                throw new InRowChangingEventException($"名为{key}的列不支持赋予{typeof(T).Name}类型的值", ex.GetInnermostException());
            }
        }

    }
}