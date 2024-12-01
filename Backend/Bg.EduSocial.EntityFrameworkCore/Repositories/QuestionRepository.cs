using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.EntityFrameworkCore.EFCore;

namespace Bg.EduSocial.EFCore.Repositories
{
    public class QuestionRepository: WriteRepository<QuestionEntity>, IQuestionRepository
    {
        public QuestionRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<QuestionEntity> GetById(Guid questionID)
        {
            var context = _unitOfWork.GetContext();
            var question = Context.Questions.Where(question => question.question_id == questionID).FirstOrDefault();
            return question;
        }


        public async Task<IEnumerable<QuestionEntity>> GetByTestID(Guid testID)
        {
            //var questions = Context.QuestionTests.Where(qt=> qt.TestID == testID).Include(qt => qt.Question).ThenInclude(q => q.op).Select(qu => qu.Question).ToList();
           // var questions = Context.QuestionTests.Where(qt => qt.test_id == testID).Include(qt => qt.Question).ThenInclude(q => q).Select(qu => qu.Question).ToList();
            return default;
        }
    }
}
