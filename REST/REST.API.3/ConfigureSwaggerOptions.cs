using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace REST.API._3
{
    public class ConfigureSwaggerOptions
    //: IConfigureNamedOptions<SwaggerGenOptions>
    {
        //private readonly IApiVersionDescriptionProvider provider;

        //public ConfigureSwaggerOptions(
        //    IApiVersionDescriptionProvider provider)
        //{
        //    this.provider = provider;
        //}

        //public void Configure(SwaggerGenOptions options)
        //{
        //    // Agrega documentación de Swagger para cada versión del API encontrada
        //    foreach (var description in provider.ApiVersionDescriptions)
        //    {
        //        options.SwaggerDoc(
        //            description.GroupName,
        //            CreateVersionInfo(description));
        //    }
        //}

        //public void Configure(string name, SwaggerGenOptions options)
        //{
        //    Configure(options);
        //}

        //private OpenApiInfo CreateVersionInfo(
        //        ApiVersionDescription description)
        //{
        //    var info = new OpenApiInfo()
        //    {
        //        Title = "Prueba API",
        //        Version = description.ApiVersion.ToString()
        //    };

        //    if (description.IsDeprecated)
        //    {
        //        info.Description += " Esta versión de API esta obsoleta.";
        //    }

        //    return info;
        //}
    }
}
