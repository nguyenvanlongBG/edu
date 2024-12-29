using Bg.EduSocial.Domain.Cores;
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
    }
}
