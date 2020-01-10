using MySqlHelpr.Attributes;

namespace MySqlHelpr.Model
{
    /// <summary>
    /// 用户权限表
    /// </summary>
    public class IdentityRole : BaseModel
    {       
        [Required]
        public string OrganizationId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string Discriminator { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(1, 6)]
        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string Type { get; set; }

    }
}
