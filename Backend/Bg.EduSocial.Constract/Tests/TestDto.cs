using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Tests
{
    public class TestDto: TestEntity
    {
        public List<QuestionDto> questions { get; set; }
    }
}
