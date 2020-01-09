using System;
using System.Collections.Generic;
using System.Text;

namespace MySqlHelpr.Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public DateTime RegTime { get; set; }

        public string Email { get; set; }

        public string Nick { get; set; }

        public string DeliveryID { get; set; }
    }
}
