namespace REST.API.Simple.Middleware
{
    public static class Middleware1Extension
    {
        public static IApplicationBuilder MiddlewareExample(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Middleware1>();
        }

    }
}
