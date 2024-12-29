using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class TestRepository : WriteRepository<TestEntity>, ITestRepository
    {
        private readonly IQuestionRepository _questionRepository;
        public TestRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<QuestionEntity>> GetQuestionOfTest(Guid testId)
        {
            var questions = Context.Questions.ToList();
            return questions;
        }
    }
}
