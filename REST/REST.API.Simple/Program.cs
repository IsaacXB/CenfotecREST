var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hi from delegate 1-A");
    await next();
    await context.Response.WriteAsync("Hi from delegate 1-B");
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Hola from delegate 2");
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Hola from delegate 3 - Not working because previous delegate is of type Run");
});


app.Run();
