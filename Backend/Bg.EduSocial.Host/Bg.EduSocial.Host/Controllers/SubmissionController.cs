using Bg.EduSocial.Application;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Submissions;
using Bg.EduSocial.Domain.Submissions;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionController : WriteController<Submission, SubmissionDto, SubmissionEditDto, SubmissionEditDto>
    {
        private readonly ISubmissionService _submissionService;
        public SubmissionController(ISubmissionService submissionService) : base(submissionService)
        {
            _submissionService = submissionService;
        }
        [HttpGet("mark")]
        public virtual async Task<IActionResult?> MarkSubmission(Guid id)
        {
            try
            {
                var response = await _submissionService.MarkSubmission(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(default);
            }
        }
    }
}
