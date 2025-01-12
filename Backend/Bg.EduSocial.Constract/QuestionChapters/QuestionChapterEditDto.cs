using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract.QuestionChapters
{
    public class QuestionChapterEditDto : QuestionChapterEntity, IRecordState
    {
        public ModelState State { get; set; } = ModelState.View;
    }
}
