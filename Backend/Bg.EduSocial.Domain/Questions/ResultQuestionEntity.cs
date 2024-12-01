using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("result_question")]
    public class ResultQuestionEntity
    {
        [Key]
        public Guid result_question_id { get; set; }
        public Guid question_id { get; set; }
        public string content { get; set; }
    }
}
