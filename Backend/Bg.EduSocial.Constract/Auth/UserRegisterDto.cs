using Bg.EduSocial.Domain.Shared.Roles;

namespace Bg.EduSocial.Constract.Auth
{
    public class UserRegisterDto
    {
        public string name { get; set; } = string.Empty;
        public string user_name { get; set; }
        public string password { get; set; }
        public Role role_id { get; set; }
    }
}
