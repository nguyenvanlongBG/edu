using Bg.EduSocial.Application.Exeptions;
using System.Net;

namespace Bg.EduSocial.Host.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            // Kiểm tra có phải lỗi không tìm thấy
            if (exception is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                NotFoundException notFoundException = (NotFoundException)exception;
                await context.Response.WriteAsync(
                    text: new BaseException(notFoundException.ErrorCode, notFoundException.UserMessages, notFoundException.DevMessages, context.TraceIdentifier, exception.HelpLink)
                    .ToString() ?? ""
                    );
                // Kiểm tra lỗi có phải lỗi Validate
            }
        }
    }
}
