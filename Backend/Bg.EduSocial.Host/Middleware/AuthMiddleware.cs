using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using System.Security.Claims;

namespace Bg.EduSocial.Host.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            using (var scope = context.RequestServices.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var contextService = scope.ServiceProvider.GetRequiredService<IContextService>();
                var contextData = new ContextData
                {
                    user = new UserDto
                    {
                        user_id = Guid.NewGuid(),
                    }
                };
                contextService.SetContextData(contextData);

                // Sử dụng userService và contextService tại đây
                var userIdStr = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!String.IsNullOrEmpty(userIdStr))
                {
                    Guid.TryParse(userIdStr, out Guid userId);
                    var user = await userService.GetById<UserDto>(userId);
                    if (user != null)
                    {
                        contextData.user = user;
                        contextService.SetContextData(contextData);
                    }
                }
            }

            await _next(context);
        }

    }

}
