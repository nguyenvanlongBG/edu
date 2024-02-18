using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class TestRepository : WriteRepository<Test>, ITestRepository
    {
        private readonly IQuestionRepository _questionRepository;
        public TestRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
        public override async Task<Test> GetById(Guid id)
        {
            var test = await Context.Tests.Where(test => test.ID == id).Include(test => test.QuestionTests)
        .ThenInclude(questionTest => questionTest.Question)
            .ThenInclude(question => question.Answers).Select(test => new Test
            {
                ID = test.ID,
                Name = test.Name,
                StartTime = test.StartTime,
                FinishTime = test.FinishTime,
                Questions = test.QuestionTests.Select(questionTest => questionTest.Question).ToList()
            }).FirstOrDefaultAsync();
            if (test is not null) { return test; };
            return default;
        }
    }
}
