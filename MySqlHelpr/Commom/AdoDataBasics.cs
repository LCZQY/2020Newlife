using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MySqlHelpr.Commom
{
    /// <summary>
    /// 链接基础层
    /// </summary>
    public class AdoDataBasics
    {
        private static readonly string connectionString = "Server=localhost;Database =zqy;Uid=root;Pwd='';charset=utf8;";

        /// <summary>
        /// 读取执行数据并返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static async Task<DataTable> GetDataTableAsync(string sql, CommandType type, params MySqlParameter[] pars)
        {
            using (MySqlConnection myConnection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter apter = new MySqlDataAdapter(sql, myConnection))
                {
                    if (pars != null)
                    {
                        apter.SelectCommand.Parameters.AddRange(pars);
                    }
                    apter.SelectCommand.CommandType = type;
                    DataTable data = new DataTable();
                    await apter.FillAsync(data);
                    return data;
                }
            }
        }

        /// <summary>
        /// 修改增加调用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="pars"></param>
        /// <returns></returns>

        public static async Task<bool> ExecuteNonqueryAsync(string sql, CommandType type, params MySqlParameter[] pars)
        {
            Console.WriteLine($"SQL:{sql}");
            using (MySqlConnection myConnection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand commd = new MySqlCommand(sql, myConnection))
                {
                    if (pars != null)
                    {
                        commd.Parameters.AddRange(pars);
                    }
                    commd.CommandType = type;
                    await myConnection.OpenAsync();
                    return await commd.ExecuteNonQueryAsync() > 0;
                }
            }
        }



    }
}
