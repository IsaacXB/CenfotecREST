
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using REST.API._2.Middleware;
using REST.API._2.Security;
using REST.Database.Context;
using REST.Database.Models;
using REST.Database.Services;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My API",
        Description = "ASP.NET Core Web API to manage users data",
        TermsOfService = new Uri("https://www.google.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Isaac Chaves",
            Url = new Uri("https://www.google.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://www.google.com/license")
        }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, AuthHandler>("BasicAuthentication", null);

var appSettingsSect = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSect);

// Configuración del JWT
var appSettings = appSettingsSect.Get<AppSettings>();
// Lo codificamos en una matriz de Bytes
var key = Encoding.ASCII.GetBytes(appSettings.Key);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    // Agregar tiempo de vida al token
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var _myCors = "AllowedOrigins";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: _myCors, policy =>
//    {
//        //policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
//        //    .AllowAnyHeader().AllowAnyMethod();

//        policy.WithOrigins("https://localhost:7007")
//            .AllowAnyHeader().AllowAnyMethod();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1", policy =>
    {
        policy.WithOrigins("https://localhost:7007");
    });

    options.AddPolicy("Policy2", policy =>
    {
        policy.WithOrigins("https://mysite1.com").WithMethods("GET");
    });

    options.AddPolicy("Policy2", policy =>
    {
        policy.WithOrigins("https://mysite2.com").WithMethods("POST","PUT","DELETE");
    });
});


var app = builder.Build();

app.ErrorHandler();

//app.UseCors(_myCors);
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ErrorDevelopment");
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = Text.Plain;

            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandlerFeature != null)
            {
                switch (exceptionHandlerFeature.Error)
                {
                    case FileNotFoundException:
                        await context.Response.WriteAsync("File Not Found.");
                        break;
                    default:
                        await context.Response.WriteAsync("An error has ocurred.");
                        break;
                }
            }
            else
            {
                await context.Response.WriteAsync("An error has ocurred.");
            }
        });
    });


    app.UseSwagger( options =>
    {
        options.RouteTemplate = "/myapi/sawgger/{documentname}/swagger.json";
    });
    app.UseSwaggerUI( options =>
    {
        //options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
        options.SwaggerEndpoint("/myapi/sawgger/v1/swagger.json", "My API Version: 1");
        options.RoutePrefix = "myapi/swagger";
        options.InjectStylesheet("/css/swagger-ui/mystyle.css");

        options.SupportedSubmitMethods(
            new Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod[]
            {
                Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod.Get,
                Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod.Put,
                Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod.Post

            });
    });
}
else
{
    //app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
