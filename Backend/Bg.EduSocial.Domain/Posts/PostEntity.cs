using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain.Posts
{
    [Table("post")]
    public class PostEntity: BaseEntity
    {
        [Key]
        public Guid post_id { get; set; }
        public Guid user_id { get; set; }
        public string content { get; set; }
        public Guid group_id { get; set; }
    }
}
