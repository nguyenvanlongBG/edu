using Bg.EduSocial.Domain.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Classes
{
    public class Classroom : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<TestClass> TestClasses { get; set; }
        public ICollection<UserClassroom> UserClasses { get; set; }
    }
}
