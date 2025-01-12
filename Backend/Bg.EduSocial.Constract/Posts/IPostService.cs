using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Posts;

namespace Bg.EduSocial.Constract.Posts
{
    public interface IPostService : IWriteService<PostEntity, PostDto, PostEditDto>
    {
    }
}
