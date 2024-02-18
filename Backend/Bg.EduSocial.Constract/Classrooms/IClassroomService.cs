using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Classrooms
{
    public interface IClassroomService : IWriteService<Classroom, ClassroomDto, ClassroomEditDto, ClassroomEditDto>
    {
    }
}
