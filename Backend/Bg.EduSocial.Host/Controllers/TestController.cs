using Bg.EduSocial.Application;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.FileQuestion;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
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
            await questionService.HandleAddQuestion(data);
            return Ok(data);

        }
        [HttpGet("{testId}/do")]
        public async Task<IActionResult> GetTest(Guid testId)
        {
            var questions = await _service.GetQuestionOfTest(testId);
            return Ok(questions);

        }
    }
}
