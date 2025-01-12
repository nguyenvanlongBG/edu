using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : WriteController<IEnrollmentClassService, EnrollmentClassEntity, EnrollmentClassDto, EnrollmentClassEditDto>
    {
        public EnrollmentController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpGet("{classroom_id}/enroll")]
        public async Task<IActionResult> EnrollOfClassroom(Guid classroom_id)
        {
            var exam = await _service.EnrollmentClassesAsync(classroom_id);
            return Ok(exam);
        }
    }
}
