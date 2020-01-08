using System;
using System.Collections.Generic;
using System.Text;

namespace MySqlHelpr.Interface
{

    public class MapperObject
    {

        /// <summary>
        ///拼接查询SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string SelectSql<T>(T oObject) where T : class
        {
            string sql = "";
            Type type = oObject.GetType();
            foreach (var proper in type.GetProperties())
            {
                sql += proper.Name + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            return $"select {sql} from {nameof(T)};";
        }

        /// <summary>
        /// 拼接带查询条件的SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oObject"></param>
        /// <param name="id">查找Id</param>
        /// <returns></returns>
        public static string SelectSql<T>(T oObject, string id) where T : class
        {
            string sql = "";
            Type type = oObject.GetType();
            foreach (var proper in type.GetProperties())
            {
                sql += proper.Name + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            return $"select {sql} from {nameof(T)}; Where Id = {id}";
        }


        public static string InsertSql<T>(T oObject)
        {
            return null;
        }

    }
}
