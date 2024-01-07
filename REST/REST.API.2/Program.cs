
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using REST.API._2.Middleware;
using REST.Database.Context;
using REST.Database.Services;
using static System.Net.Mime.MediaTypeNames;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.ErrorHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/ErrorDevelopment");
    //app.UseExceptionHandler(exceptionHandlerApp =>
    //{
    //    exceptionHandlerApp.Run(async context =>
    //    {
    //        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    //        context.Response.ContentType = Text.Plain;

    //        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
    //        if (exceptionHandlerFeature != null)
    //        {
    //            switch (exceptionHandlerFeature.Error)
    //            {
    //                case FileNotFoundException:
    //                    await context.Response.WriteAsync("File Not Found.");
    //                    break;
    //                default:
    //                    await context.Response.WriteAsync("An error has ocurred.");
    //                    break;
    //            }
    //        }
    //        else
    //        {
    //            await context.Response.WriteAsync("An error has ocurred.");
    //        }
    //    });
    //});
    

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
