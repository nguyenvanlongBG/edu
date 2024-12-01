using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Constract
{
    public interface IUserService : IWriteService<UserEntity, UserDto, UserEditDto>
    {
    }
}
