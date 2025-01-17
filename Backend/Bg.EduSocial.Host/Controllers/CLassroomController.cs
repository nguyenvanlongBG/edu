using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("class-of-user")]
        [Authorize]
        public async Task<IActionResult> GetClassOfUser()
        {
            var questions = await _service.GetClassroomsOfUser();
            return Ok(questions);

        }
        [HttpPost("paging-classroom")]
        [Authorize]
        public async Task<IActionResult> PagingClassroom([FromBody] PagingParam param)
        {
            var questions = await _service.PagingClassroom(param);
            return Ok(questions);

        }
    }
}
