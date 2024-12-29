using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract
{
    public class QuestionEditDto: QuestionEntity, IRecordState
    {
        public ICollection<OptionEditDto> options { get; set; } = new List<OptionEditDto>();
        public List<object> object_content;
        public ICollection<ResultQuestionDto> results { get; set; } = new List<ResultQuestionDto>();
        public decimal point { get; set; }
        public ModelState State { get; set; } = ModelState.View;
    }
}
