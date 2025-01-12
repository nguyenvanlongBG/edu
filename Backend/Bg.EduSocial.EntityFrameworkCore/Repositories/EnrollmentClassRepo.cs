using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class EnrollmentClassRepo : WriteRepository<EnrollmentClassEntity>, IEnrollmentClassRepository
    {
        public EnrollmentClassRepo(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
