using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.Modes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("option")]
    public class OptionEntity :BaseEntity
    {
        [Key]
        public Guid option_question_id { get; set; }
        public string content { get; set; } = string.Empty;
        public Guid question_id { get; set; }
    }
}
