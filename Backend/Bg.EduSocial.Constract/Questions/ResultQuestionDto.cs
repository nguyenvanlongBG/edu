using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Questions
{
    public class ResultQuestionDto
    {
        public Guid result_question_id { get; set; }
        public Guid question_id { get; set; }
        public string content { get; set; }
    }
}
