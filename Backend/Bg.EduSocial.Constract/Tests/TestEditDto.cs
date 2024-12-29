using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract
{
    public class TestEditDto : TestEntity, IRecordState
    {
        public ICollection<QuestionEditDto> questions { get; set; } = new List<QuestionEditDto>();

        public ModelState State { get; set; } = ModelState.View;
    }
}
