using Bg.EduSocial.Domain.Shared.Classrooms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Classrooms
{
    public class EnrollmentClassDto
    {
        [Key]
        public Guid enrollmen_class_id { get; set; }
        public Guid user_id { get; set; }
        public Guid classroom_id { get; set; }
        public EnrollmentClassType Status { get; set; }
    }
}
