using Microsoft.EntityFrameworkCore;
using REST.Database.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DBConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
