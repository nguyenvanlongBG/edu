using System.ComponentModel.DataAnnotations;

namespace Bg.EduSocial.Constract.QuestionChapters
{
    public class QuestionChapterDto
    {
        [Key]
        public Guid question_chapter_id { get; set; }
        public Guid chapter_id { get; set; }
        public Guid question_id { get; set; }
        public Guid subject_id { get; set; }
    }
}
