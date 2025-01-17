using Bg.EduSocial.Domain.Cores;

namespace Bg.EduSocial.Domain.Exams
{
    public interface IExamRepo : IWriteRepository<ExamEntity>
    {
        Task<ExamEntity> ExamDoing(Guid testId, Guid userId);
    }
}
