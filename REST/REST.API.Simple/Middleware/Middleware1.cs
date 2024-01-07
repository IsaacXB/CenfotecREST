namespace REST.API.Simple.Middleware
{
    public class Middleware1
    {
        private readonly RequestDelegate _next; 

        public Middleware1(RequestDelegate next)
        {
            _next = next;
        }   

        public async Task InvokeAsync(HttpContext context)
        {
            //await context.Response.WriteAsync("Hi from custom Middleware example");
            await _next(context);
        }
    }
}
