using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Questions;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CLassroomController : WriteController<Classroom, ClassroomDto, ClassroomEditDto, ClassroomEditDto>
    {
        private readonly IClassroomService _classroomService;
        public CLassroomController(IClassroomService classroomService) : base(classroomService)
        {
            _classroomService = classroomService;
        }
    }
}
