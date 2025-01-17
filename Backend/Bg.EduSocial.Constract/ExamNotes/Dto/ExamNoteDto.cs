using System.ComponentModel.DataAnnotations;

namespace Bg.EduSocial.Constract.ExamNotes
{
    public class ExamNoteDto
    {
        [Key]
        public Guid exam_note_id { get; set; }
        public Guid exam_id { get; set; }
        public Guid question_id { get; set; }
        public string content { get; set; }
    }
}
