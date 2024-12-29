using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Mvc;
using Bg.EduSocial.Constract.Exams;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : WriteController<IExamService, ExamEntity, ExamDto, ExamEditDto>
    {
        public ExamController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpPost("new-exam")]
        public async Task<IActionResult> NewExam([FromBody] Guid testId)
        {
            var exam =  await _service.NewExam(testId);
            return Ok(exam);
        }
        [HttpPost("test-of-exam")]
        public async Task<IActionResult> GetTestOfExam([FromBody] Guid examId)
        {
            var exam = await _service.TestOfExam(examId);
            return Ok(exam);
        }
        [HttpPut("do")]
        public async Task<IActionResult> ExamDoing([FromBody] ExamEditDto exam)
        {
            var examResult = await _service.DoExam(exam);
            return Ok(examResult);
        }
        [HttpGet("{examId}/history")]
        public async Task<IActionResult> HistoryExam(Guid examId)
        {
            var testHistory = await _service.HistoryExam(examId);
            return Ok(testHistory);
        }
        [HttpPut("mark")]
        public async Task<IActionResult> ExamMarking([FromBody] ExamEditDto exam)
        {
            var examResult = await _service.MarkExam(exam);
            return Ok(examResult);
        }
    }
}
