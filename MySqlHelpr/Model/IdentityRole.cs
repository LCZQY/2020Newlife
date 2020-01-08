using System;
using System.Collections.Generic;
using System.Text;

namespace MySqlHelpr.Model
{
    /// <summary>
    /// 用户权限表
    /// </summary>
    public class IdentityRole
    {
        public string Id { get; set; }

        public string OrganizationId { get; set; }

        public string ConcurrencyStamp {get;set;}

        public string Discrinminator { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string Type { get; set; }

    }
}
