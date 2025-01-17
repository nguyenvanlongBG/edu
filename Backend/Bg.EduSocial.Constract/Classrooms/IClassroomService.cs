using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Constract.Classrooms
{
    public interface IClassroomService : IWriteService<ClassroomEntity, ClassroomDto, ClassroomEditDto>
    {
        Task<List<ClassroomDto>> GetClassroomsOfUser();
        Task<List<ClassroomDto>> PagingClassroom(PagingParam param);
    }
}
