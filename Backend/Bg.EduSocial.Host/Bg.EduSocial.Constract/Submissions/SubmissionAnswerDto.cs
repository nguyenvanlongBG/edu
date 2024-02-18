using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Shared.Modes;
using Bg.EduSocial.Domain.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Submissions
{
    public class SubmissionAnswerDto
    {
        public Guid SubmissionID { get; set; }
        public Guid QuestionID { get; set; }
        public string Answer { get; set; }
        public EditMode EditMode { get; set; } = EditMode.NONE;
    }
}
