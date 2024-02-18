using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Shared.Questions;
using Bg.EduSocial.EntityFrameworkCore;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EFCore.Repositories
{
    public class QuestionRepository: WriteRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<Question> GetById(Guid questionID)
        {
            var context = _unitOfWork.GetContext();
            var question = Context.Questions.Where(question => question.ID == questionID).FirstOrDefault();
            return question;
        }

        public override async Task<IEnumerable<Question>> FilterAsync()
        {
            var data = await Context.Questions.Include(q => q.Answers).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Question>> GetByTestID(Guid testID)
        {
            var questions = Context.QuestionTests.Where(qt=> qt.TestID == testID).Include(qt => qt.Question).ThenInclude(q => q.Answers).Select(qu => qu.Question).ToList();
            return questions;
        }
    }
}
