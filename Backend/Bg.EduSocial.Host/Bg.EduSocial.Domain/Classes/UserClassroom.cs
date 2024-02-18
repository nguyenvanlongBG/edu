using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.Classrooms;
using Bg.EduSocial.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Classes
{
    public class UserClassroom : BaseEntity
    {
        public UserClassType Type { get; set; }
        public User User { get; set; }
        public Classroom Classroom { get; set; }
    }
}
