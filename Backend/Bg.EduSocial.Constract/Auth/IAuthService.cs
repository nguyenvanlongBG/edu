using Bg.EduSocial.Constract.Users;

namespace Bg.EduSocial.Constract.Auth
{
    public interface IAuthService
    {
        Task<UserTokenDto> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(UserRegisterDto user);
    }
}
