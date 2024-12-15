using Bg.EduSocial.Domain.Cores;

namespace Bg.EduSocial.Domain.Classes
{
    public interface IClassroomRepository: IWriteRepository<ClassroomEntity>
    {
        Task<ClassroomEntity> GetByClassroomCode(string classroomCode);
    }
}
