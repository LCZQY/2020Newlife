using MySql.Data.MySqlClient;
using MySqlHelpr.Model;
using System;
using System.Linq;
using System.Reflection;

namespace MySqlHelpr.Commom
{

    /// <summary>
    /// 拼接SQL语句
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class SplicingSql<T> where T: BaseModel
    {
        static SplicingSql()
        {
            Type t = typeof(T);

            _field = string.Join(",", t.GetProperties().Select(y => y.Name)); //拿到每个属性然后再用逗号隔开           
            _parameterInset = string.Join(",", t.GetProperties().Select(y => $"@{y.Name}"));
            _parameterUpdata = string.Join(",", t.GetProperties().Where(y=> !y.Name.Equals("Id")).Select(y => $"{y.Name} =@{y.Name}"));

        }

        /// <summary>
        /// 参数化后的字符串
        /// </summary>
        private static string _parameterInset { get; set; }
        /// <summary>
        /// 拼接字段字符串
        /// </summary>
        private static string _field { get; set; }
        /// <summary>
        /// 携带参数化字段数据的字符串
        /// </summary>
        private static string _parameterUpdata { get; set; }


        /// <summary>
        ///查询SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string SelectSql() 
        {                     
            return $"select {_field} from {typeof(T).Name};";
        }

        /// <summary>
        /// 带条件查询SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oObject"></param>
        /// <param name="id">查找Id</param>
        /// <returns></returns>
        public static string SelectSql(string id) 
        {
            return $"select {_field} from {typeof(T).Name} Where Id = '{id}';";
        }


        /// <summary>
        /// 新增SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string InsertSql() 
        {
            return $"insert into {typeof(T).Name}( {_field} ) values ( {_parameterInset});";
        }

        /// <summary>
        /// 修改SQL
        /// </summary>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string UpdataSql()
        {
            return $"update  {typeof(T).Name} set {_parameterUpdata} ;";
        }

        /// <summary>
        /// 待条件修改SQL
        /// </summary>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string UpdataSql(string id)
        {
            return $"update  {typeof(T).Name} set {_parameterUpdata}  where Id = '{id}';";
        }

        /// <summary>
        /// 待条件修改SQL
        /// </summary>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string RemoveSql(string id)
        {
            return $"delete from {typeof(T).Name} where Id = '{id}';";
        }


        /// <summary>
        /// 防止Sql注入，组合参数化SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MySqlParameter[] ParameterArray(T t)
        {
            Type type = t.GetType();
            return type.GetProperties().Select(y => new MySqlParameter($"@{y.Name}", y.GetValue(t) ?? DBNull.Value)).ToArray();
        }

    }
}
