using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.Constants;
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
        public Guid subject_id { get; set; } = Constant.MathSubjectId;
        public QuestionLevel? level { get; set; }
        public string content { get; set; }
        public string? chapter_ids { get; set; }
        public int from { get; set; } = 1; // 0 Thư viện, 1 từ đề thi

    }
}
