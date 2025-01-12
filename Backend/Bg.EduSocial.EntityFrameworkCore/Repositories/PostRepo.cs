using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Posts;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class PostRepo : WriteRepository<PostEntity>, IPostRepo
    {
        public PostRepo(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
