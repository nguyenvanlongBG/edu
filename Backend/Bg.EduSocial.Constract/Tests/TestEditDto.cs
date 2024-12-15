using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract
{
    public class TestEditDto : TestEntity, IRecordState
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public ICollection<QuestionEditDto> Questions { get; set; } = new List<QuestionEditDto>();

        public ModelState State { get; set; } = ModelState.View;
    }
}
