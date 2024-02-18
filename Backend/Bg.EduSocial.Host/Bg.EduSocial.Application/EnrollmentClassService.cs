using AutoMapper;
using Bg.EduSocial.Application;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Classrooms
{
    public class EnrollmentClassService : WriteService<EnrollmentClass, EnrollmentClassDto, EnrollmentClassEditDto, EnrollmentClassEditDto>, IEnrollmentClassService
    {
        public EnrollmentClassService(IUnitOfWork<EduSocialDbContext> unitOfWork, IEnrollmentClassRepository enrollmentClassRepository, IMapper mapper) : base(unitOfWork, enrollmentClassRepository, mapper)
        {
        }
    }
}
