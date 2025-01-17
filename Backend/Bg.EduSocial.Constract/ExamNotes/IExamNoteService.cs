using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Constract.ExamNotes
{
    public interface IExamNoteService : IWriteService<ExamNoteEntity, ExamNoteDto, ExamNoteEditDto>
    {
    }
}
