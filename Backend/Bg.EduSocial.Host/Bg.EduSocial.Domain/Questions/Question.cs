using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.Domain.Shared.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Questions
{
    public class Question: BaseEntity
    {
        public string Description { get; set; }
        public QuestionType QuestionType { get; set; }
        public string ResultsIDs { get; set; }
        public ICollection<Answer> Answers { get; set; }
        [JsonIgnore]
        public ICollection<QuestionTest> QuestionTests { get; set; }
    }
}
