using Bg.EduSocial.Constract.Posts;
using Bg.EduSocial.Domain.Posts;

namespace Bg.EduSocial.Application
{
    public class PostService : WriteService<IPostRepo, PostEntity, PostDto, PostEditDto>, IPostService
    {
        public PostService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
