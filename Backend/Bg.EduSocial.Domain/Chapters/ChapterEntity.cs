using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("chapter")]
    public class ChapterEntity: BaseEntity
    {
        [Key]
        public Guid chapter_id { get; set; }
        public Guid subject_id { get; set; }
        public string name { get; set; }
    }
}
