using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Shared.Questions;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<QuestionEntity>> GetRandomQuestion(Guid chapter_id, int recognition, int comprehension, int application, int advanced_application)
        {
            var result = new List<QuestionEntity>();
            var joinedData = await Records
            .GroupJoin(
                Context.QuestionChapters,            // Bảng QuestionChapters
                r => r.question_id,                  // Khóa từ bảng Records
                qc => qc.question_id,                // Khóa từ bảng QuestionChapters
                (r, qcs) => new { Record = r, QuestionChapters = qcs.DefaultIfEmpty() }
            )
            .SelectMany(
                x => x.QuestionChapters,             // Mở rộng tập hợp kết quả từ GroupJoin
                (x, qc) => new
                {
                    Record = x.Record,               // Dữ liệu từ Records
                    QuestionChapter = qc             // Dữ liệu từ QuestionChapters
                }
            )
            .Where(x => x.QuestionChapter != null && x.QuestionChapter.chapter_id == chapter_id) // Lọc theo chapter_id
            .Select(x => new
            {
                x.Record,                            // Dữ liệu từ bảng Records
                x.QuestionChapter                    // Dữ liệu từ bảng QuestionChapters
            })
            .ToListAsync();
            var level1Questions = joinedData
                .Where(x => x.Record.level == QuestionLevel.Recognition)
                .OrderBy(_ => Guid.NewGuid()) // Random
                .Take(recognition)
                .Select(x => x.Record);

            var level2Questions = joinedData
                .Where(x => x.Record.level == QuestionLevel.Comprehension)
                .OrderBy(_ => Guid.NewGuid()) // Random
                .Take(comprehension)
                .Select(x => x.Record);

            var level3Questions = joinedData
                .Where(x => x.Record.level == QuestionLevel.Application)
                .OrderBy(_ => Guid.NewGuid()) // Random
                .Take(application)
                .Select(x => x.Record);

            var level4Questions = joinedData
                .Where(x => x.Record.level == QuestionLevel.AdvancedApplication)
                .OrderBy(_ => Guid.NewGuid()) // Random
                .Take(advanced_application)
                .Select(x => x.Record);
            result.AddRange(level1Questions);
            result.AddRange(level2Questions);
            result.AddRange(level3Questions);
            result.AddRange(level4Questions);
            return result;
        }
    }
}
