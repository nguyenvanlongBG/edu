using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Questions;

namespace Bg.EduSocial.Application
{
    public class ResultQuestionService : WriteService<IResultQuestionRepo, ResultQuestionEntity, ResultQuestionDto, ResultQuestionEditDto>, IResultQuestionService
    {
        public ResultQuestionService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
