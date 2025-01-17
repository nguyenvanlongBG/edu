using Bg.EduSocial.Constract.ExamNotes;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Application
{
    public class ExamNoteService : WriteService<IExamNoteRepo, ExamNoteEntity, ExamNoteDto, ExamNoteEditDto>, IExamNoteService
    {
        public ExamNoteService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
