using Bg.EduSocial.Constract.Answers;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract.Exams
{
    public class ExamEditDto: ExamEntity, IRecordState
    {
        public ModelState State { get; set; } = ModelState.View;
        public List<AnswerEditDto> answers { get; set; }
    }
}
