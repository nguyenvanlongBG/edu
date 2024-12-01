using Bg.EduSocial.Constract.Auth;
using Bg.EduSocial.Constract.Authen;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bg.EduSocial.Application
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AccountEntity> _signInManager;
        private UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IConfiguration configuration, SignInManager<AccountEntity> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false);
            if (!result.Succeeded)
            {
                return new LoginResponse { Success = false, Error = "401", Token = string.Empty }; ;
            }
            
            var claims = new[] {
                new Claim(ClaimTypes.Name, loginRequest.Username),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiryInDays"]));
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiry,
                signingCredentials: creds);
            return new LoginResponse { Success = true, Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
