using REST.Database.Utilities;
using System.Net;
using System.Text.Json;

namespace REST.API.Homework2.Middleware
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
                var response = context.Response;
                response.ContentType = "application/json";
                Response<string> responseModel;

                switch (ex)
                {
                    case KeyNotFoundException e:
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        responseModel = new Response<string>(string.Format("Error: {0}", ex.Message));
                        break;
                    default:
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        responseModel = new Response<string>(string.Format("Error: {0}", ex.Message));
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
                //await context.Response.WriteAsync(ex.Message);
                //context.Response.Headers.Clear();
            }

        }
    }
}
