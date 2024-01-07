using REST.API.Simple.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MiddlewareExample();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Map("map1", HandleMapTest1);
//app.Map("map2", HandleMapTest2);

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hi from delegate 1-A");
//    await next();
//    await context.Response.WriteAsync("Hi from delegate 1-B");
//});

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hi from delegate 2");
//});

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hi from delegate 3 - Not working because previous delegate is of type Run");
//});


app.Run();

//static void HandleMapTest1(IApplicationBuilder applicationBuilder)
//{
//    applicationBuilder.Run( async context =>
//    {
//        await context.Response.WriteAsync("Hi from Mapped test 1");
//    });
//}

//static void HandleMapTest2(IApplicationBuilder applicationBuilder)
//{
//    applicationBuilder.Run(async context =>
//    {
//        await context.Response.WriteAsync("Hi from Mapped test 2");
//    });
//}
