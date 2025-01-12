using Bg.EduSocial.Constract.Auth;
using Bg.EduSocial.Constract.Authen;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        protected readonly IServiceProvider _serviceProvider;
        public AuthController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest param)
        {
            var authService = _serviceProvider.GetRequiredService<IAuthService>();
            var token = await authService.LoginAsync(param.user_name, param.password);
            return Ok(token);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto param)
        {
            var authService = _serviceProvider.GetRequiredService<IAuthService>();
            var success = await authService.RegisterAsync(param);
            return Ok(success);
        }
    }
}
