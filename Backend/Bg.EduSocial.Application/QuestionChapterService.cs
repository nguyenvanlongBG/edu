using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.QuestionChapters;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Application
{
    public class QuestionChapterService : WriteService<IQuestionChapterRepo, QuestionChapterEntity, QuestionChapterDto, QuestionChapterEditDto>, IQuestionChapterService
    {
        public QuestionChapterService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
