using Microsoft.EntityFrameworkCore;
using REST.API.Homework2.Middleware;
using REST.Database.Context;
using REST.Database.Services;
using Microsoft.AspNetCore.Mvc.Versioning;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    // Default Version
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);

    // When no version is specified, default version will be used
    options.AssumeDefaultVersionWhenUnspecified = true;

    // Will show available versions to clients
    options.ReportApiVersions = true;

    // Header will show the api version
    var multiVersionReader = new HeaderApiVersionReader("x-version");

    // used to read the api version specified by a client
    options.ApiVersionReader = multiVersionReader;

    // to use version in header

});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddScoped<IUserService, UserService>();

var _myCors = "AllowedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1", policy =>
    {
        policy.WithOrigins("https://localhost:7007");
    });
});

var app = builder.Build();

app.ErrorHandler();

app.UseCors();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
