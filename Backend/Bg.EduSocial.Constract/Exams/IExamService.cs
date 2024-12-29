using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Constract.Exams
{
    public interface IExamService : IWriteService<ExamEntity, ExamDto, ExamEditDto>
    {
        Task<ExamEditDto> NewExam(Guid testId);
        Task<TestDto> TestOfExam(Guid examId);
        Task<ExamEditDto> DoExam(ExamEditDto exam);
        Task<TestDto> HistoryExam(Guid examId);
        Task<ExamEditDto> MarkExam(ExamEditDto exam);
        ExamEditDto MarkExam(ExamEditDto exam, TestDto test);
    }
}
