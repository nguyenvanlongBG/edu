using System.ComponentModel.DataAnnotations;

namespace Bg.EduSocial.Constract.Answers
{
    public class AnswerDto
    {
        [Key]
        public Guid answer_id { get; set; }
        public string content { get; set; }
        public Guid question_id { get; set; }
        public Guid exam_id { get; set; }
        public decimal point { get; set; }
    }
}
