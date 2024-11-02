using AutoMapper;
using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Submissions;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application
{
    public class ClassroomService : WriteService<Classroom, ClassroomDto, ClassroomEditDto, ClassroomEditDto>, IClassroomService
    {
        public ClassroomService(IUnitOfWork<EduSocialDbContext> unitOfWork, IClassroomRepository classroomRepository, IMapper mapper) : base(unitOfWork, classroomRepository, mapper)
        {
        }
    }
}
