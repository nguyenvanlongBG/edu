using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Shared.Questions;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public async Task<List<QuestionEntity>> GetRandomQuestion(Guid userId, Guid chapter_id, int recognition, int comprehension, int application, int advanced_application)
        {
            var result = new List<QuestionEntity>();
            var joinedData = await Records.Where(r => (r.user_id == Guid.Empty || r.user_id == userId) && r.from == 0 && !string.IsNullOrEmpty(r.chapter_ids) && r.chapter_ids.Contains(chapter_id.ToString()))
            .ToListAsync();
            var level1Questions = joinedData
                .Where(x => x.level == QuestionLevel.Recognition)
                .OrderBy(_ => Guid.NewGuid()) // Random
                .Take(recognition)
                .Select(x => x);

            var level2Questions = joinedData
                .Where(x => x.level == QuestionLevel.Comprehension)
                .OrderBy(_ => Guid.NewGuid()) // Random
                .Take(comprehension)
                .Select(x => x);

            var level3Questions = joinedData
                .Where(x => x.level == QuestionLevel.Application)
                .OrderBy(_ => Guid.NewGuid()) // Random
                .Take(application)
                .Select(x => x);

            var level4Questions = joinedData
                .Where(x => x.level == QuestionLevel.AdvancedApplication)
                .OrderBy(_ => Guid.NewGuid()) // Random
                .Take(advanced_application)
                .Select(x => x);
            result.AddRange(level1Questions);
            result.AddRange(level2Questions);
            result.AddRange(level3Questions);
            result.AddRange(level4Questions);
            return result;
        }
    }
}
