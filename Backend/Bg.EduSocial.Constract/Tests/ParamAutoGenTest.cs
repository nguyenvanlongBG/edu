using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Tests
{
    public class ChapterGenQuestionConfig
    {
        public Guid chapter_id { get; set; }
        public int recognition { get; set; }
        public int comprehension { get; set; }
        public int application { get; set; }
        public int advanced_application { get; set; }
    }
    public class ParamAutoGenTest
    {
        public List<ChapterGenQuestionConfig> chapters { get; set; }
    }
}
