using MySql.Data.MySqlClient;
using System;
using System.Reflection;

namespace MySqlHelpr.Commom
{

    public static class SplicingSql<T> where T : class
    {

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
        /// 组合参数化写法字符串
        /// </summary>
        public static void CreateParameter(T oObject)
        {
            _field = "";
            _parameterInset = "";
            _parameterUpdata = "";
            Type t = oObject.GetType();
            foreach (var pi in t.GetProperties())
            {
                _field += pi.Name + ",";
                _parameterInset += "@" + pi.Name + ",";
                _parameterUpdata += pi.Name + "=@" + pi.Name + ",";
            }
            _field = _field.Substring(0, _field.LastIndexOf(','));
            _parameterInset = _parameterInset.Substring(0, _parameterInset.LastIndexOf(','));
            _parameterUpdata = _parameterUpdata.Substring(0, _parameterUpdata.LastIndexOf(','));
        }

        /// <summary>
        ///查询SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string SelectSql(T oObject)
        {
            CreateParameter(oObject);
            return $"select {_field} from {typeof(T).Name};";
        }

        /// <summary>
        /// 带条件查询SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oObject"></param>
        /// <param name="id">查找Id</param>
        /// <returns></returns>
        public static string SelectSql(T oObject, string id)
        {
            CreateParameter(oObject);
            return $"select {_field} from {typeof(T).Name} Where Id = '{id}';";
        }


        /// <summary>
        /// 新增SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string InsertSql(T oObject)
        {
            CreateParameter(oObject);
            return $"insert into {typeof(T).Name}( {_field} ) values ( {_parameterInset});";
        }

        /// <summary>
        /// 修改SQL
        /// </summary>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string UpdataSql(T oObject)
        {
            CreateParameter(oObject);
            return $"update  {typeof(T).Name} set {_parameterUpdata} ;";
        }

        /// <summary>
        /// 待条件修改SQL
        /// </summary>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string UpdataSql(T oObject, string id)
        {
            CreateParameter(oObject);
            return $"update  {typeof(T).Name} set {_parameterUpdata}  where Id = '{id}';";
        }

        /// <summary>
        /// 待条件修改SQL
        /// </summary>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static string RemoveSql(T oObject, string id)
        {
            CreateParameter(oObject);
            return $"delete from {typeof(T).Name} where Id = '{id}';";
        }


        /// <summary>
        /// 防止Sql注入，组合参数化SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MySqlParameter[] ParameterArray(T model)
        {
            Type t = model.GetType();
            try
            {
                MySqlParameter[] pars = new MySqlParameter[t.GetProperties().Length];
                var i = 0;
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    var value = pi.GetValue(model, null) ?? "";//用pi.GetValue获得值
                    var type = (value.GetType() ?? typeof(object)).Name;//获得属性的类型                            
                    switch (type)
                    {
                        case "String":
                            pars[i] = new MySqlParameter("@" + pi.Name, MySqlDbType.VarChar);
                            break;
                        case "DateTime":
                            pars[i] = new MySqlParameter("@" + pi.Name, MySqlDbType.DateTime);
                            break;
                        case "Int32":
                            pars[i] = new MySqlParameter("@" + pi.Name, MySqlDbType.Int32);
                            break;
                        case "Decimal":
                            pars[i] = new MySqlParameter("@" + pi.Name, MySqlDbType.Decimal);
                            break;
                        case "Double":
                            pars[i] = new MySqlParameter("@" + pi.Name, MySqlDbType.Float);
                            break;
                        case "Boolean":
                            pars[i] = new MySqlParameter("@" + pi.Name, MySqlDbType.Bit);
                            break;
                        default:
                            pars[i] = new MySqlParameter("@" + pi.Name, MySqlDbType.VarChar);
                            break;
                    }
                    pars[i].Value = value ?? null;
                    i += 1;
                }

                return pars;
            }
            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
        }

    }
}
