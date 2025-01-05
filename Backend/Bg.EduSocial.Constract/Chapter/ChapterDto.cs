using System.ComponentModel.DataAnnotations;

namespace Bg.EduSocial.Constract.Chapter
{
    public class ChapterDto
    {
        [Key]
        public Guid chapter_id { get; set; }
        public Guid subject_id { get; set; }
        public string name { get; set; }
    }
}
