using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.Roles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("user")]
    public class UserEntity: BaseEntity
    {
        [Key]
        public Guid user_id { get; set; }
        public string name { get; set; } = string.Empty;
        public string user_name { get; set; }
        public string password { get; set; }
        public Role role_id { get; set; }
    }
}
