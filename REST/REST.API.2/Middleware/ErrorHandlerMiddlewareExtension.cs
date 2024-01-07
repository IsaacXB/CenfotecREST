namespace REST.API._2.Middleware
{
    public static class ErrorHandlerMiddlewareExtension
    {
        public static IApplicationBuilder ErrorHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }

    }
}
