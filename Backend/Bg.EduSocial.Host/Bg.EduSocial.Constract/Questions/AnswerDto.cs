using Bg.EduSocial.Domain.Shared.Modes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Questions
{
    public class AnswerDto
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public Guid QuestionID { get; set; }
        public EditMode EditMode { get; set; } = EditMode.NONE;
    }
}
