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

// to use with API version in URL
//builder.Services.AddVersionedApiExplorer(setup =>
//{
//    setup.GroupNameFormat = "'v'VVV";
//    setup.SubstituteApiVersionInUrl = true;
//});

builder.Services.AddSwaggerGen();

//builder.Service.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(options =>
    //{
    //    foreach (var description in provider  ApiVersionDescriptions)
    //    {
    //        options.SwaggerEndpoint($"/swagger/{description.GorupName}/swagger.json");
    //    }
    //});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
