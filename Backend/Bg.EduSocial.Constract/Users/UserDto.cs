using Bg.EduSocial.Domain.Shared.Roles;
using System.ComponentModel.DataAnnotations;

namespace Bg.EduSocial.Constract
{
    public class UserDto
    {
        [Key]
        public Guid user_id { get; set; }
        public string name { get; set; } = string.Empty;
        public string user_name { get; set; }
        public Role role_id { get; set; }
    }
}
