using Bg.EduSocial.Domain.Shared.ModelState;
using Bg.EduSocial.Domain.Shared.Modes;

namespace Bg.EduSocial.Constract
{
    public class OptionDto
    {
        public Guid option_question_id { get; set; }
        public string content { get; set; } = string.Empty;
        public List<object> object_content { get; set; }
        public Guid question_id { get; set; }
    }
}
