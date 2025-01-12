using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Exams;
using Bg.EduSocial.Domain.Shared;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class ExamRepo : WriteRepository<ExamEntity>, IExamRepo
    {
        public ExamRepo(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<ExamEntity> ExamDoing(Guid testId)
        {
            return Records.Where(exam => exam.status == ExamStatus.Doing && exam.test_id == testId).FirstOrDefault();
        }
    }
}
