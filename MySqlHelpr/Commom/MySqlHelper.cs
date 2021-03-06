﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using MySqlHelpr.Model;

namespace MySqlHelpr.Commom
{
    public static class MySqlHelper
    {

        /// <summary>
        /// 查询操作
        /// </summary>
        public static async Task<List<T>> GetListAsync<T>(this T model) where T : BaseModel
        {
            string sql = SplicingSql<T>.SelectSql();
            var da = await AdoDataBasics.GetDataTableAsync(sql, CommandType.Text);
            List<T> list = new List<T> { };
            if (da.Rows.Count > 0)
            {
                foreach (DataRow row in da.Rows)
                {
                    list.Add(LoadEntity<T>(model, row) as T);
                }
            }
            return list;
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        public static async Task<List<T>> GetListAsync<T>(this T model, string id) where T : BaseModel
        {
            string sql = SplicingSql<T>.SelectSql(id);
            var da = await AdoDataBasics.GetDataTableAsync(sql, CommandType.Text);
            List<T> list = new List<T> { };
            if (da.Rows.Count > 0)
            {
                foreach (DataRow row in da.Rows)
                {
                    list.Add(LoadEntity<T>(model, row) as T);
                }
            }
            return list;
        }

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<bool> InSertAsync<T>(this T model) where T : BaseModel
        {
            string sql = SplicingSql<T>.InsertSql();
            var parp = SplicingSql<T>.ParameterArray(model);
            return await AdoDataBasics.ExecuteNonqueryAsync(sql, CommandType.Text, parp);
        }

        /// <summary>
        /// 批量新增操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<bool> InSertRangeAsync<T>(this List<T> model) where T : BaseModel
        {
            return true;
        }



        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateAsync<T>(this T model) where T : BaseModel
        {
            string sql = SplicingSql<T>.UpdataSql();
            var parp = SplicingSql<T>.ParameterArray(model);
            return await AdoDataBasics.ExecuteNonqueryAsync(sql, CommandType.Text, parp);
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateAsync<T>(this T model, string id) where T : BaseModel
        {
            string sql = SplicingSql<T>.UpdataSql(id);
            var parp = SplicingSql<T>.ParameterArray(model);
            return await AdoDataBasics.ExecuteNonqueryAsync(sql, CommandType.Text, parp);
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<bool> RemoveAsync<T>(this T model, string id) where T : BaseModel
        {
            string sql = SplicingSql<T>.RemoveSql(id);
            var parp = SplicingSql<T>.ParameterArray(model);
            return await AdoDataBasics.ExecuteNonqueryAsync(sql, CommandType.Text, parp);
        }


        /// <summary>
        /// 初始化实体
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="omodel"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private static Object LoadEntity<T1>(T1 omodel, DataRow row)
        {
            var type = omodel.GetType();
            var entity = Activator.CreateInstance(typeof(T1));
            foreach (var pror in type.GetProperties())
            {
                var rowValue = row[pror.Name];
                var dataType = (rowValue.GetType() ?? typeof(object)).Name;//获得属性的类型                            
                switch (dataType)
                {
                    case "String":
                        rowValue = rowValue is DBNull ? string.Empty : rowValue.ToString();
                        break;
                    case "DateTime":
                        rowValue = rowValue != DBNull.Value ? Convert.ToDateTime(rowValue) : (DateTime?)null;
                        break;
                    case "Int32":
                        rowValue = rowValue != DBNull.Value ? Convert.ToInt32(rowValue) : (int?)null;
                        break;
                    case "Decimal":
                        rowValue = rowValue != DBNull.Value ? Convert.ToDecimal(rowValue) : (decimal?)null;
                        break;
                    case "Double":
                        rowValue = rowValue != DBNull.Value ? Convert.ToDouble(rowValue) : (double?)null;
                        break;
                    case "Boolean":
                        rowValue = rowValue != DBNull.Value ? Convert.ToBoolean(rowValue) : (bool?)null;
                        break;
                    case "UInt64":
                        rowValue = rowValue != DBNull.Value ? Convert.ToBoolean(rowValue) : (bool?)null;
                        break;
                    default:
                        throw new Exception("该数据类型不能够被转化");
                }
                pror.SetValue(entity, rowValue);
            }
            return entity;
        }
    }

}
