using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Constract.Questions
{
    public interface IQuestionService: IWriteService<QuestionEntity, QuestionDto, QuestionEditDto>
    {
        Task HandleAddQuestion(List<QuestionDto> questions);
    }
}
