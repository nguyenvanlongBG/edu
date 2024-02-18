using Bg.EduSocial.Domain.Shared.Modes;
using Bg.EduSocial.Domain.Shared.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Questions
{
    public class QuestionEditDto
    {
        public Guid? ID { get; set; }
        public string Description { get; set; }
        public QuestionType QuestionType { get; set; }
        public string ResultsIDs { get; set; }
        public ICollection<AnswerEditDto> Answers { get; set; } = new List<AnswerEditDto>();
        public EditMode EditMode { get; set; } = EditMode.NONE;
    }
}
