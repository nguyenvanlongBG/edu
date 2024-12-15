using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class ClassroomRepository : WriteRepository<ClassroomEntity>, IClassroomRepository
    {
        public ClassroomRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ClassroomEntity> GetByClassroomCode(string classroomCode)
        {
            if (string.IsNullOrEmpty(classroomCode)) return default;
            return await Records.FirstOrDefaultAsync(classroom => classroom.classroom_code == classroomCode);
        }
    }
}
