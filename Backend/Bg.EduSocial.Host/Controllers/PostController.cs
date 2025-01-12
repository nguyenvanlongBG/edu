using Bg.EduSocial.Constract.Posts;
using Bg.EduSocial.Domain.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostController : WriteController<IPostService, PostEntity, PostDto, PostEditDto>
    {
        public PostController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
