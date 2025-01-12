using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Posts;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract.Posts
{
    public class PostEditDto : PostEntity, IRecordState
    {
        public ModelState State { get; set; } = ModelState.View;
    }
}
