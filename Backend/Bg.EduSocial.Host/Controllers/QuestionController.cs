using Bg.EduSocial.Constract;
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
    }
}
