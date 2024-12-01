using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("subject")]
    public class SubjectEntity: BaseEntity
    {
        [Key]
        public Guid subject_id { get; set; }
        public string subject_name { get; set; }
    }
}
