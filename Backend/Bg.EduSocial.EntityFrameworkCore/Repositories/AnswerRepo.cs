using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class AnswerRepo : WriteRepository<AnswerEntity>, IAnswerRepo
    {
        public AnswerRepo(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
