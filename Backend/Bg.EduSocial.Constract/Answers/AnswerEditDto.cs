using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract.Answers
{
    public class AnswerEditDto : AnswerEntity, IRecordState
    {
        public ModelState State { get; set; } = ModelState.View;
    }
}
