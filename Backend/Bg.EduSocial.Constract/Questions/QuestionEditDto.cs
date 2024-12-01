using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract
{
    public class QuestionEditDto: QuestionEntity, IRecordState
    {
        public ICollection<OptionDto> options { get; set; } = new List<OptionDto>();
        public List<object> object_content;
        public ModelState State { get; set; } = ModelState.View;
    }
}
