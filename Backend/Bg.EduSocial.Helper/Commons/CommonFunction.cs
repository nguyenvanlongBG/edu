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
        public static object ConvertToType(object value, Type targetType)
        {
            if (value == null)
                return null;

            if (targetType == typeof(string))
                return value.ToString();

            if (targetType == typeof(int) && int.TryParse(value.ToString(), out var intValue))
                return intValue;

            if (targetType == typeof(double) && double.TryParse(value.ToString(), out var doubleValue))
                return doubleValue;

            if (targetType == typeof(DateTime) && DateTime.TryParse(value.ToString(), out var dateTimeValue))
                return dateTimeValue;

            if (targetType == typeof(bool) && bool.TryParse(value.ToString(), out var boolValue))
                return boolValue;

            throw new InvalidOperationException($"Cannot convert value '{value}' to type {targetType.Name}");
        }

    }
}
