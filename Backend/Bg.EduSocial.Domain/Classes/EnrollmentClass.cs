using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.Classrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Classes
{
    public class EnrollmentClass: BaseEntity
    {
        public Guid UserID { get; set; }
        public Guid ClassID { get; set; }
        public EnrollmentClassType Status { get; set; }
    }
}
