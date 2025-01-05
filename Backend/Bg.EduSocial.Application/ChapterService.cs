using Bg.EduSocial.Constract.Chapter;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Chapters;

namespace Bg.EduSocial.Application
{
    public class ChapterService : WriteService<IChapterRepo, ChapterEntity, ChapterDto, ChapterEditDto>, IChapterService
    {
        public ChapterService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
