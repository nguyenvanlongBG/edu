using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Questions;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : WriteController<Question, QuestionDto, QuestionEditDto, QuestionEditDto>
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService) : base(questionService)
        {
            _questionService = questionService;
        }
    }
}
