using MySql.Data.MySqlClient;
using MySqlHelpr.Commom;
using System;

namespace MySqlHelpr
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //查询
            {
                string sql = "select * from users;";
                await sql.QueryAsync();
                

            }



        }
    }
}
