using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Constract.Classrooms
{
    public interface IEnrollmentClassService: IWriteService<EnrollmentClassEntity, EnrollmentClassDto, EnrollmentClassEditDto>
    {
        Task<List<EnrollmentClassDto>> EnrollmentClassesAsync(Guid classroom_id);
    }
}
