using Microsoft.AspNetCore.Http;

namespace Common.Middleware
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalResponse = context.Response.Body;

            using (var newResponseBody = new MemoryStream())
            {
                context.Response.Body = newResponseBody;
                await _next(context);

                context.Response.Body = originalResponse;
                await context.Response.WriteAsync(
                    new { message = "Request successful", statusCode = context.Response.StatusCode }.ToString());
            }
        }
    }
}
