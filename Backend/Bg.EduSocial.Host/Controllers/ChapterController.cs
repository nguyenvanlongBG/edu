using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Mvc;
using Bg.EduSocial.Constract.Chapter;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChapterController : WriteController<IChapterService, ChapterEntity, ChapterDto, ChapterEditDto>
    {
        public ChapterController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
