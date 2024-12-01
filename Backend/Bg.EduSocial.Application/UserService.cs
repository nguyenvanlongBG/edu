using Bg.EduSocial.Constract;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Application
{
    public class UserService : WriteService< IUserRepository,UserEntity, UserDto, UserEditDto>, IUserService
    {
        public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
