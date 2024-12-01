using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Auths;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bg.EduSocial.Helper.Commons
{
    public static class CommonFunction
    {
        
        public static async Task<UserEntity?> GetUserFromToken()
        {
            //var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            //if (claimsIdentity is not null)
            //{
            //    // Lấy ra claim chứa thông tin về tên người dùng
            //    var userNameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);
            //    if (userNameClaim is not null)
            //    {
            //        var user = await _authManager.GetUserByName(userNameClaim.Value);
            //        return user;
            //    }
            //}
            return default;
        }
    }
}
