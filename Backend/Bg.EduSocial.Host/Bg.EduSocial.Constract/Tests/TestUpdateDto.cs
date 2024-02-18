﻿using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Tests
{
    public class TestUpdateDto
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public ICollection<QuestionEditDto> Questions { get; set; } = new List<QuestionEditDto>();    
    }
}
