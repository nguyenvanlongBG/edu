using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Shared.Modes;
using Bg.EduSocial.Domain.Shared.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Questions
{
    public class QuestionDto
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public QuestionType QuestionType { get; set; }
        public string ResultsIDs { get; set; }
        public ICollection<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
        public EditMode EditMode { get; set; } = EditMode.NONE;
    }
}
