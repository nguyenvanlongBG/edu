using Bg.EduSocial.Domain.Cores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Questions
{
    public class Answer: BaseEntity
    {
        public string Description { get; set; }
        public Guid QuestionID { get; set; }
        public Question Question { get; set; }
    }
}
