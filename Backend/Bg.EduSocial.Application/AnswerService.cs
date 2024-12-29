using Bg.EduSocial.Constract.Answers;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Questions;

namespace Bg.EduSocial.Application
{
    public class AnswerService : WriteService<IAnswerRepo, AnswerEntity, AnswerDto, AnswerEditDto>, IAnswerService
    {
        public AnswerService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
