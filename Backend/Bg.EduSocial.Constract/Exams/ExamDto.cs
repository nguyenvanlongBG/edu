using System.ComponentModel.DataAnnotations;

namespace Bg.EduSocial.Constract.Exams
{
    public class ExamDto
    {
        [Key]
        public Guid exam_id { get; set; }
        public Guid user_id { get; set; }
        public Guid test_id { get; set; }
        public decimal point { get; set; }
    }
}
