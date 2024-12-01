using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("answer")]
    public class AnswerEntity: BaseEntity
    {
        [Key]
        public Guid answer_id { get; set; }
        public string content { get; set; }
        public Guid question_id { get; set; }
        public Guid exam_id { get; set; }
        public decimal point { get; set; }
        public string note { get; set; }

    }
}
