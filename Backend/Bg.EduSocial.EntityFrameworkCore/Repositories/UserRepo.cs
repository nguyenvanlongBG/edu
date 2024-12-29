using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class UserRepo : WriteRepository<UserEntity>, IUserRepository
    {
        public UserRepo(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
