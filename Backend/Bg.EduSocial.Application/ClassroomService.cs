using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Classes;

namespace Bg.EduSocial.Application
{
    public class ClassroomService : WriteService<IClassroomRepository,ClassroomEntity, ClassroomDto, ClassroomEditDto>, IClassroomService
    {
        public ClassroomService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
