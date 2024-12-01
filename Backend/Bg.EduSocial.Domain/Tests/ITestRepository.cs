using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Tests
{
    public interface ITestRepository : IWriteRepository<TestEntity>
    {
        Task<List<QuestionEntity>> GetQuestionOfTest(Guid testId);
    }
}
