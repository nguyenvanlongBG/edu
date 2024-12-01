using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Shared.ModelState;
using Bg.EduSocial.Domain.Shared.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Classrooms
{
    public class ClassroomEditDto: IRecordState
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ModelState State { get; set; } = ModelState.View;
    }
}
