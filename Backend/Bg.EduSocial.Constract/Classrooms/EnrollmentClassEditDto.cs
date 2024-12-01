using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Shared.ModelState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Classrooms
{
    public class EnrollmentClassEditDto: IRecordState
    {
        public ModelState State { get; set; } = ModelState.View;
    }
}
