using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("question_chapter")]
    public class QuestionChapterEntity: BaseEntity
    {
        [Key]
        public Guid questio_chapter_id { get; set; }
        public Guid chapter_id { get; set; }
        public Guid question_id { get; set; }
        public Guid subject_id { get; set; }

    }
}
