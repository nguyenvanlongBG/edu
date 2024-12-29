using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application
{
    public class QuestionTestService : WriteService<IQuestionTestRepository, QuestionTestEntity, QuestionTestEditDto, QuestionTestEditDto>, IQuestionTestService
    {
        public QuestionTestService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
