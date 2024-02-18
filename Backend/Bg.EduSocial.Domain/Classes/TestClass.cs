using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Classes
{
    public class TestClass: BaseEntity
    {
        public Guid ClassroomID { get; set; }
        public Guid TestID { get; set; }
        public Classroom Classroom { get; set; }
        public Test Test { get; set; }
    }
}
