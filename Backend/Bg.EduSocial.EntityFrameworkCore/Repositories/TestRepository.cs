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
        public override async Task<TestEntity> GetById(Guid id)
        {
            //    var test = await Context.Tests.Where(test => test.ID == id).Include(test => test.QuestionTests)
            //.ThenInclude(questionTest => questionTest.Question)
            //    .ThenInclude(question => question.Answers).Select(test => new Test
            //    {
            //        ID = test.ID,
            //        Name = test.Name,
            //        StartTime = test.StartTime,
            //        FinishTime = test.FinishTime,
            //        Questions = test.QuestionTests.Select(questionTest => questionTest.Question).ToList()
            //    }).FirstOrDefaultAsync();
            //    if (test is not null) { return test; };
            return default;
        }
        public async Task<List<QuestionEntity>> GetQuestionOfTest(Guid testId)
        {
            var questions = Context.Questions.ToList();
            return questions;
        }
    }
}
