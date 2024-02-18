using Bg.EduSocial.Domain.Auths;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Helper.Commons
{
    public static class CommonFunction
    {
        public static IHttpContextAccessor _httpContextAccessor;
        public static AuthManager _authManager;
        public static void Initialize(IHttpContextAccessor httpContextAccessor, AuthManager authManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authManager = authManager;
        }

        public static async Task<User?> GetUserFromToken()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity is not null)
            {
                // Lấy ra claim chứa thông tin về tên người dùng
                var userNameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);
                if (userNameClaim is not null)
                {
                    var user = await _authManager.GetUserByName(userNameClaim.Value);
                    return user;
                }
            }
            return default;
        }
    }
}
