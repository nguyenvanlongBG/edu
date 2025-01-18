using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Constract.Exams
{
    public interface IExamService : IWriteService<ExamEntity, ExamDto, ExamEditDto>
    {
        Task<ExamEditDto> NewExam(Guid testId);
        Task<TestDto> TestOfExam(Guid examId);
        Task<TestDoingDto> LastExam(Guid testId);
        Task<ExamEditDto> DoExam(ExamEditDto exam);
        Task<ExamEditDto> SubmitExam(ExamEditDto exam);

        Task<TestDto> HistoryExam(Guid examId);
        Task<ExamEditDto> MarkExam(Guid examId);
        ExamEditDto MarkExam(ExamEditDto exam, TestDto test);
        Task<ExamEditDto> NoteExam(ExamEditDto exam);

        Task<List<ExamEditDto>> prepareExamMark(List<ExamEditDto> exams);
    }
}
