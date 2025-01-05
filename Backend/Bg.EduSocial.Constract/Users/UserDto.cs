using Bg.EduSocial.Domain.Shared.Roles;

namespace Bg.EduSocial.Constract
{
    public class UserDto
    {
        public Guid user_id { get; set; }
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public Role role { get; set; }
    }
}
