using Bg.EduSocial.Domain.Shared.Modes;

namespace Bg.EduSocial.Constract.Classrooms
{
    public class TestClassDto
    {
        public Guid ID { get; set; }
        public Guid ClassroomID { get; set; }
        public Guid TestID { get; set; }
        public EditMode EditMode { get; set; } = EditMode.NONE;
    }
}
