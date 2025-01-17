using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Mvc;
using Bg.EduSocial.Constract.Exams;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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
        [HttpPut("note")]
        public async Task<IActionResult> NoteExam([FromBody] ExamEditDto exam)
        {
            var examResult = await _service.NoteExam(exam);
            return Ok(examResult);
        }
        [HttpPut("submit")]
        public async Task<IActionResult> Submit([FromBody] ExamEditDto exam)
        {
            var examResult = await _service.SubmitExam(exam);
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
