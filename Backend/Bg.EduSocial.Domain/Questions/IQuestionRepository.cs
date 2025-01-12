using Bg.EduSocial.Domain.Cores;

namespace Bg.EduSocial.Domain.Questions
{
    public interface IQuestionRepository : IWriteRepository<QuestionEntity>
    {
        Task<IEnumerable<QuestionEntity>> GetByTestID(Guid testID);
        Task<List<QuestionEntity>> GetRandomQuestion(Guid chapter_id, int recognition, int comprehension, int application, int advanced_application);
    }
}
