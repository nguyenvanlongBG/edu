using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("test")]
    public class TestEntity : BaseEntity
    {
        [Key]
        public Guid test_id { get; set; }
        public string name { get; set; }
        public DateTime start_time { get; set; }
        public DateTime? finish_time { get; set; }
    }
}
