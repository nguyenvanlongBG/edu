using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Questions
{
    public interface IQuestionService: IWriteService<Question, QuestionDto, QuestionEditDto, QuestionEditDto>
    {
    }
}
