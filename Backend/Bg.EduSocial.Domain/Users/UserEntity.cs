using Bg.EduSocial.Domain.Cores;
using Microsoft.AspNetCore.Identity;

namespace Bg.EduSocial.Domain
{
    public class UserEntity: BaseEntity
    {
        public Guid user_id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
