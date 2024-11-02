using Bg.EduSocial.Constract.Authen;
using Bg.EduSocial.Domain.Users;
using Bg.EduSocial.Helper.Commons;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        public LoginController(IConfiguration configuration, SignInManager<User> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);
            if (!result.Succeeded)
            {
                return NotFound();
            }
            var claims = new[] {
                new Claim(ClaimTypes.Name, login.Username), 
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
            return Ok(new LoginResponse { Success = true, Token = new JwtSecurityTokenHandler().WriteToken(token)});
                
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Authen()
        {
            var user = await CommonFunction.GetUserFromToken();
            return Ok("OK");

        }
        
        [HttpGet("ok")]
        public async Task<IActionResult> Done()
        {
            return Ok("Done Authen");

        }
        [HttpPost("uploadFile")]
        public IActionResult CreateQuestionsFromFile(IFormFile file, [FromForm] string testId)
        {
            var data = EditorFunction.GetLatexFromFile(file);
            return Ok(data);

        }
    }
}
