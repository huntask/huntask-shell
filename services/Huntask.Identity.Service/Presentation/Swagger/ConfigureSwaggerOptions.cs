
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Huntask.Identity.Service.Presentation.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{

  private readonly IApiVersionDescriptionProvider provider;

  public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
  {
    ArgumentNullException.ThrowIfNull(provider, nameof(provider));

    this.provider = provider;
  }

  public void Configure(SwaggerGenOptions options)
  {
    foreach (var description in provider.ApiVersionDescriptions)
    {
      options.SwaggerDoc(description.GroupName, new OpenApiInfo
      {
        Title = "Huntask.Identity.Api",
        Version = description.ApiVersion.ToString(),
        Description = "Huntask Identity Service API"
      });
    }
  }
}
