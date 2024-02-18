using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Shared.Classrooms;
using Bg.EduSocial.Domain.Shared.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Classrooms
{
    public class UserClassroomDto
    {
        public Guid ID { get; set; }    
        public Guid UserID { get; set; }
        public Guid ClassroomID { get; set; }
        public UserClassType Type { get; set; }
        public EditMode EditMode { get; set; } = EditMode.NONE;
    }
}
