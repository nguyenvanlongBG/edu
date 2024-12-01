using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract
{
    public class UserEditDto: IRecordState
    {
        public Guid ID { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ModelState State { get; set; } = ModelState.View;
    }
}
