using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
namespace MySqlHelpr.Commom
{
    public static class MySqlConnections
    {   
        //创建connection连接对象  
        private static MySqlConnection myConnnect = null;
        //创建command对象      
        private static MySqlCommand command = null;

        private static readonly string connectionString = "Server=localhost;Database =zqy;Uid=root;Pwd='';charset=utf8;";


        /// <summary>
        /// 查询
        /// </summary>
        public static async Task QueryAsync(this string sql)
        {
            MySqlDataReader reader = null;
            using (myConnnect = new MySqlConnection(connectionString))
            {
                try
                {
                    await myConnnect.OpenAsync();
                    command = new MySqlCommand(sql, myConnnect);
                    reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine($"UserId: {reader["UserId"]}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw;
                }
                finally
                {
                    await reader.CloseAsync();
                    await myConnnect.CloseAsync();
                }
            }
        }


        /// <summary>
        /// 新增/修改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<bool> InSertOrUpdateAsync(this string sql)
        {
            //建立数据库连接  
            using (myConnnect = new MySqlConnection(connectionString))
            {
                myConnnect.Open();
                //启动一个事务  
                using (MySqlTransaction transaction = myConnnect.BeginTransaction())
                {
                    using (command = myConnnect.CreateCommand())
                    {
                        try
                        {
                            command.Transaction = transaction;  //为命令指定事务  
                            command.CommandText = sql;
                            command.ExecuteNonQuery();
                            transaction.Commit();    //事务提交                             
                        }
                        catch (Exception)
                        {

                            transaction.Rollback(); //事务回滚  
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }

}
