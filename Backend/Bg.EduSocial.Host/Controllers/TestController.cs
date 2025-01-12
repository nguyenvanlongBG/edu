using Bg.EduSocial.Application;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.FileQuestion;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : WriteController<ITestService, TestEntity, TestDto, TestEditDto>
    {
        public TestController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpPost("upload")]
        public IActionResult CreateQuestionsFromFile(FormFile? file)
        {
            return Ok(default);

        }
        [HttpPost("uploadFile")]
        public async Task<IActionResult> CreateQuestionsFromFile(IFormFile file, [FromForm] string? testId)
        {
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var fileQuestionService = _serviceProvider.GetRequiredService<IFileQuestionService>();
            var configQuestion = new SplitQuestion
            {
                RegexQuestion = @"\bBài\b",
                RegexAnswer = @"[A-Z]\.",
                RegexResult = @"[A-Z]\*",
            };

            var data = fileQuestionService.GetQuestionFromFile(file, configQuestion);
            return Ok(data);

        }
        [HttpGet("{testId}/do")]
        public async Task<IActionResult> GetTestDo(Guid testId)
        {
            var questions = await _service.HandleGetDoTest(testId);
            return Ok(questions);

        }
        [HttpGet("test-of-user")]
        [Authorize]
        public async Task<IActionResult> GetTestOfUser()
        {
            var questions = await _service.GetTestOfUserAsync();
            return Ok(questions);

        }
        [HttpGet("{testId}/mark")]
        public async Task<IActionResult> MarkTest(Guid testId)
        {
            var exams = await _service.MarkTest(testId);
            return Ok(exams);
        }
        [HttpGet("{testId}/exam-mark")]
        public async Task<IActionResult> GetExamMax(Guid testId)
        {
            var exams = await _service.GetExamMarkAsync(testId);
            return Ok(exams);
        }
        [HttpGet("{testId}/edit")]
        public async Task<IActionResult> GetQuestionTestEdit(Guid testId)
        {
            var questions = await _service.GetQuestionOfTestEditAsync(testId);
            return Ok(questions);

        }
        [HttpPost("auto-gen")]
        public async Task<IActionResult> GetAutoGenQuestion([FromBody] ParamAutoGenTest param)
        {
            var questions = await _service.GenAutoTest(param);
            return Ok(questions);

        }
        [HttpGet("{testId}/history")]
        public async Task<IActionResult> GetExamsHistory(Guid testId)
        {
            var exams = await _service.GetExamUserHistory(testId);
            return Ok(exams);

        }
    }
}
