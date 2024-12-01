using Bg.EduSocial.Application;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Classes;

namespace Bg.EduSocial.Constract.Classrooms
{
    public class EnrollmentClassService : WriteService<IEnrollmentClassRepository, EnrollmentClassEntity, EnrollmentClassDto, EnrollmentClassEditDto>, IEnrollmentClassService
    {
        public EnrollmentClassService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
