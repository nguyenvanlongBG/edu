using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : WriteController<IQuestionService,QuestionEntity, QuestionDto, QuestionEditDto>
    {
        public QuestionController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpPost("library")]
        public async Task<IActionResult> InsertQuestionLibrary([FromBody] List<QuestionEditDto> questions)
        {
            var questionsResult = await _service.InsertQuestionLibrary(questions);
            return Ok(questionsResult);
        }
        [HttpPost("library/paging")]
        public async Task<IActionResult> PagingQuestionLibrary([FromBody] PagingParam pagingParam)
        {
            var questionsResult = await _service.PagingQuestionLibraryAsync(pagingParam);
            return Ok(questionsResult);
        }
    }
}
