using Bg.EduSocial.Application;
using Bg.EduSocial.Constract.Authen;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Helper.Commons;
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
        private readonly SignInManager<AccountEntity> _signInManager;
        private readonly IQuestionService _questionService;
        public LoginController(IConfiguration configuration, SignInManager<AccountEntity> signInManager, IQuestionService questionService)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _questionService = questionService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.user_name, login.password, false, false);
            if (!result.Succeeded)
            {
                return NotFound();
            }
            var claims = new[] {
                new Claim(ClaimTypes.Name, login.user_name), 
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
            //var user = await CommonFunction.GetUserFromToken();
            return Ok("OK");

        }
        
        [HttpGet("ok")]
        public async Task<IActionResult> Done()
        {
            return Ok("Done Authen");

        }
    }
}
