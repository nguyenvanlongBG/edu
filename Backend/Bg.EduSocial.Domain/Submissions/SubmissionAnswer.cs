using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Submissions
{
    public class SubmissionAnswer: BaseEntity
    {
        public Guid SubmissionID { get; set; }
        public Guid QuestionID { get; set; }
        public string Answer { get; set; }
        public Submission Submission { get; set; }
        public Question Question { get; set; }
    }
}
