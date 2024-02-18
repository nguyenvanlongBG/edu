using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Submissions
{
    public class Submission: BaseEntity
    {
        public Guid UserID { get; set; }
        public Guid TestID { get; set; }
        public Test? Test { get; set; }
        public float Point { get; set; } = 0;
        public ICollection<SubmissionAnswer> SubmissionAnswers { get; set; } = new List<SubmissionAnswer>();
    }
}
