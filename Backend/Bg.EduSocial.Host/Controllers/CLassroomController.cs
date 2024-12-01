using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CLassroomController : WriteController<IClassroomService,ClassroomEntity, ClassroomDto, ClassroomEditDto>
    {
        private readonly IClassroomService _classroomService;
        public CLassroomController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
