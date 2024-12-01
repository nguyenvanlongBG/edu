using System.ComponentModel.DataAnnotations;

namespace Bg.EduSocial.Domain.Cores
{
    public class BaseEntity
    {
        public string created_by { get; set; } = string.Empty;
        public DateTime? created_date { get; set; }
        public string modified_by { get; set; } = string.Empty;
        public DateTime? modified_date { get; set; }
    }
}
