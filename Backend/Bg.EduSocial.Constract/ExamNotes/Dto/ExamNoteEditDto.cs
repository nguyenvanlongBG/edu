using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract.ExamNotes
{
    public class ExamNoteEditDto : ExamNoteEntity, IRecordState
    {
        public ModelState State { get; set; } = ModelState.View;
    }
}
