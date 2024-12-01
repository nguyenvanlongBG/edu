using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("test_classroom")]
    public class TestClassroomEntity: BaseEntity
    {
        [Key]
        public Guid test_classroom_id { get; set; }
        public Guid classroom_id { get; set; }
        public Guid test_id { get; set; }
    }
}
