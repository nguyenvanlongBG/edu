using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.Questions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("question")]
    public class QuestionEntity: BaseEntity
    {
        [Key]
        public Guid question_id { get; set; }
        public Guid user_id { get; set; }

        public QuestionType type { get; set; }
        public Guid subject_id { get; set; } = Guid.Empty;
        public QuestionLevel level { get; set; }
        public string content { get; set; }
        public string? chapter_ids { get; set; }

    }
}
