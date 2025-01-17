using AutoMapper;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Auth;
using Bg.EduSocial.Constract.Users;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.ModelState;
using Bg.EduSocial.Helper.Commons;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bg.EduSocial.Application
{
    public class AuthService : IAuthService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public AuthService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }

        // Đăng nhập và tạo JWT Token
        public async Task<UserTokenDto> LoginAsync(string username, string password)
        {
            var userService = _serviceProvider.GetRequiredService<IUserService>();
            var filters = new List<FilterCondition> { new FilterCondition
            {
                Field = "user_name",
                Operator = FilterOperator.Equal,
                Value = username
            }};
            var resultUsers = await userService.FilterAsync<UserEntity>(filters);
            UserEntity user = null;
            if (resultUsers?.Count > 0)
            {
                user = resultUsers[0];
            }
            // Kiểm tra mật khẩu (giả sử bạn đã mã hóa mật khẩu)
            if (user == null ||!VerifyPassword(user.password, password))
            {
                return default;
            }

            // Tạo JWT Token
            var token = GenerateJwtToken(user);
            var userReturn = _mapper.Map<UserEntity, UserDto>(user);
            return new UserTokenDto
            {
                user =  userReturn,
                token = token
            };
        }

        // Tạo JWT Token
        private string GenerateJwtToken(UserEntity user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.user_name),
                new Claim(ClaimTypes.PrimarySid, user.user_id.ToString()),
                new Claim(ClaimTypes.Role, user.role_id.ToString()),  // Gán vai trò cho người dùng
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1), // Token hết hạn sau 1 ngày
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterAsync(UserRegisterDto user)
        {
            var userService = _serviceProvider.GetRequiredService<IUserService>();
            var newUser = new UserEditDto
            {
                user_id = Guid.NewGuid(),
                name = user.name,
                role_id = user.role_id,
                user_name = user.user_name,
                password = HashPassword(user.password),
                State = ModelState.Insert
            };
            await userService.InsertAsync(newUser);
            return true;
        }
        public string HashPassword(string password)
        {
            // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
            return CommonFunction.HashData(password);
        }
        public bool VerifyPassword(string hashedPassword, string password)
        {
            // Kiểm tra mật khẩu khi người dùng đăng nhập
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
