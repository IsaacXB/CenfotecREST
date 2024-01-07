using System.Net;

namespace REST.API._2.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case KeyNotFoundException e:
                        context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;
                    default:
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                }
                await context.Response.WriteAsync(ex.Message);
                context.Response.Headers.Clear();
            }

        }
    }
}
