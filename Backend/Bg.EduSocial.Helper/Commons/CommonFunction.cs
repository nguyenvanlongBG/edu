using BCrypt.Net;
using Bg.EduSocial.Domain;
using DocumentFormat.OpenXml.Math;

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
        public static object? ConvertToType(object? value, Type targetType)
        {
            if (value == null) return null;

            // Lấy kiểu cơ bản nếu là nullable
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            try
            {
                // Nếu là kiểu Enum
                if (underlyingType.IsEnum)
                {
                    return Enum.Parse(underlyingType, value.ToString() ?? string.Empty, ignoreCase: true);
                }

                // Nếu là kiểu Guid
                if (underlyingType == typeof(Guid))
                {
                    return Guid.TryParse(value.ToString(), out var result) ? result : null;
                }
                if (underlyingType == typeof(String))
                {
                    return value.ToString();
                }

                // Nếu là kiểu DateTime
                if (underlyingType == typeof(DateTime))
                {
                    return DateTime.TryParse(value.ToString(), out var result) ? result : null;
                }

                // Nếu là kiểu TimeSpan
                if (underlyingType == typeof(TimeSpan))
                {
                    return TimeSpan.TryParse(value.ToString(), out var result) ? result : null;
                }

                // Nếu là kiểu Uri
                if (underlyingType == typeof(Uri))
                {
                    return Uri.TryCreate(value.ToString(), UriKind.RelativeOrAbsolute, out var result) ? result : null;
                }

                // Nếu là kiểu bool
                if (underlyingType == typeof(bool))
                {
                    return bool.TryParse(value.ToString(), out var result) ? result : null;
                }

                // Nếu là kiểu bool
                if (underlyingType == typeof(Int32))
                {
                    return int.TryParse(value.ToString(), out var result) ? result : null;
                }
                if (underlyingType == typeof(decimal))
                {
                    return decimal.TryParse(value.ToString(), out var result) ? result : null;
                }
                // Nếu là kiểu số nguyên, số thực
                if (underlyingType.IsPrimitive || underlyingType == typeof(decimal))
                {
                    return Convert.ChangeType(value, underlyingType);
                }

               
                // Các kiểu khác
                return Convert.ChangeType(value, underlyingType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Cannot convert value '{value}' to type {targetType.Name}.", ex);
            }
        }

        public static string HashData(object password)
        {
            // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
            return BCrypt.Net.BCrypt.HashPassword(password.ToString());
        }


    }
}
