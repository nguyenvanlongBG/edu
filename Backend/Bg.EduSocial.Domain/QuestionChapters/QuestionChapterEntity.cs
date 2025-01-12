using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain.QuestionChapters
{
    [Table("question_chapter")]
    public class QuestionChapterEntity: BaseEntity
    {
        [Key]
        public Guid question_chapter_id { get; set; }
        public Guid chapter_id { get; set; }
        public Guid question_id { get; set; }
        public Guid subject_id { get; set; }
    }
}
