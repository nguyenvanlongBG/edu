using System.ComponentModel.DataAnnotations;

namespace Bg.EduSocial.Constract.Posts
{
    public class PostDto
    {
        [Key]
        public Guid post_id { get; set; }
        public Guid user_id { get; set; }
        public string content { get; set; }
        public Guid group_id { get; set; }
    }
}
