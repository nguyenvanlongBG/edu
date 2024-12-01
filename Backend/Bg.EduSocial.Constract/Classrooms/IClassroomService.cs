using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Constract.Classrooms
{
    public interface IClassroomService : IWriteService<ClassroomEntity, ClassroomDto, ClassroomEditDto>
    {
    }
}
