using Bg.EduSocial.Application;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.Roles;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bg.EduSocial.Host.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }


        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            // Kiểm tra nếu endpoint có Attribute Authen
            using (var scope = context.RequestServices.CreateScope())
            {
                var contextService = scope.ServiceProvider.GetRequiredService<IContextService>();

                if (endpoint?.Metadata.GetMetadata<AuthorizeAttribute>() != null)
                {
                    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                    if (!string.IsNullOrEmpty(token))
                    {
                        try
                        {
                            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var validationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = _configuration["Jwt:Issuer"],
                                ValidAudience = _configuration["Jwt:Audience"],
                                IssuerSigningKey = new SymmetricSecurityKey(key)
                            };

                            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                            var claims = principal.Claims;

                            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                            if (Guid.TryParse(claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value, out Guid userId) && Enum.TryParse(claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value, out Role role))
                            {
                                var contextData = new ContextData
                                {
                                    user = new UserDto
                                    {
                                        user_name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                                        user_id = userId,
                                        role_id = role,
                                    }
                                };
                                contextService.SetContextData(contextData);
                            };
                        }
                        catch (SecurityTokenExpiredException)
                        {
                            context.Response.StatusCode = 401; // Unauthorized
                            await context.Response.WriteAsync("Token has expired.");
                            return;
                        }
                        catch (SecurityTokenException)
                        {
                            context.Response.StatusCode = 401; // Unauthorized
                            await context.Response.WriteAsync("Invalid token.");
                            return;
                        }
                        catch (Exception)
                        {
                            context.Response.StatusCode = 500; // Internal Server Error
                            await context.Response.WriteAsync("An error occurred during token validation.");
                            return;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 401; // Unauthorized
                        await context.Response.WriteAsync("Token is missing.");
                        return;
                    }
                } else 
                {
                    var contextData = new ContextData
                    {
                        user = new UserDto
                        {
                            user_name = string.Empty,
                            user_id = Guid.NewGuid(),
                            role_id = Role.None,
                        }
                    };
                    contextService.SetContextData(contextData);
                } 
            }

            await _next(context);
        }


    }

}
