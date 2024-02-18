using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Submissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Tests
{
    public class Test : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        [NotMapped]
        public ICollection<Question> Questions{get; set;}
        [JsonIgnore]
        public ICollection<QuestionTest> QuestionTests { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
