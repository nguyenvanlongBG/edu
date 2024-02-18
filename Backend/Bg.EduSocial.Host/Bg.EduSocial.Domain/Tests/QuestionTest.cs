using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Tests
{
    public class QuestionTest: BaseEntity
    {
        public Guid QuestionID { get; set; }
        public Guid TestID { get; set; }
        public Question Question { get; set; }
        public Test Test { get; set; }
    }
}
