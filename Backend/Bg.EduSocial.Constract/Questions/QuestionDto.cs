using Bg.EduSocial.Constract.Answers;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract
{
    public class QuestionDto: QuestionEntity, IRecordState
    {
        public ICollection<OptionDto> options { get; set; } = new List<OptionDto>();
        public AnswerDto? answer { get; set; }

        public ICollection<ResultQuestionDto> results { get; set; } = new List<ResultQuestionDto>();
        public List<object> object_content { get; set; }
        public decimal point { get; set; }
        public ModelState State { get; set; } = ModelState.View;
    }
}
