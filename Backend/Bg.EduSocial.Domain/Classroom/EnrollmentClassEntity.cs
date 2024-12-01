using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.Classrooms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("enrollment_class")]
    public class EnrollmentClassEntity: BaseEntity
    {
        [Key]
        public Guid enrollmen_class_id { get; set; }
        public Guid user_id { get; set; }
        public Guid classroom_id { get; set; }
        public EnrollmentClassType Status { get; set; }
    }
}
