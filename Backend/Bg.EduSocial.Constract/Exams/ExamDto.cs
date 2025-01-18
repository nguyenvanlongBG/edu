using Bg.EduSocial.Constract.ExamNotes;
using Bg.EduSocial.Domain.Shared;
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
        public string name { get; set; }
        public string question_ids_attention { get; set; } = string.Empty;
        public List<ExamNoteDto>? notes { get; set; }
        public ExamStatus status { get; set; }
    }
}
