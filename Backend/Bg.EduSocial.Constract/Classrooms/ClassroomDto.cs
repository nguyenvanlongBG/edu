using Bg.EduSocial.Domain.Shared.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Classrooms
{
    public class ClassroomDto
    {
        public Guid classroom_id { get; set; }
        public string name { get; set; }
        public string classroom_code { get; set; }

        public string description { get; set; }
        public string avatar { get; set; }
    }
}
