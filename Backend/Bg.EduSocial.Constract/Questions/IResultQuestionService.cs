using Bg.EduSocial.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Questions
{
    public interface IResultQuestionService : IWriteService<ResultQuestionEntity, ResultQuestionDto, ResultQuestionEditDto>
    {
    }
}
