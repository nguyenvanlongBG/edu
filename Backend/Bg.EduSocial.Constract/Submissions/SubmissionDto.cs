using Bg.EduSocial.Domain.Shared.Modes;
using Bg.EduSocial.Domain.Submissions;
using Bg.EduSocial.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Submissions
{
    public class SubmissionDto
    {
        public Guid UserID { get; set; }
        public Guid TestID { get; set; }
        public float Point { get; set; }
        public ICollection<SubmissionAnswerDto> SubmissionAnswers { get; set; }
        public EditMode EditMode { get; set; } = EditMode.NONE;
    }
}
