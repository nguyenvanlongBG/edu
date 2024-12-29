using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("question_test")]
    public class QuestionTestEntity: BaseEntity
    {
        [Key]
        public Guid question_test_id { get; set; }
        public Guid question_id { get; set; }
        public Guid test_id { get; set; }
        public decimal point { get; set; }
    }
}
