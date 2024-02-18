using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Classrooms
{
    public interface IEnrollmentClassService: IWriteService<EnrollmentClass, EnrollmentClassDto, EnrollmentClassEditDto, EnrollmentClassEditDto>
    {
    }
}
