using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("exam_note")]
    public class ExamNoteEntity : BaseEntity
    {
        [Key]
        public Guid exam_note_id { get; set; }
        public Guid exam_id { get; set; }
        public Guid question_id { get; set; }
        public string content { get; set; }

    }
}
