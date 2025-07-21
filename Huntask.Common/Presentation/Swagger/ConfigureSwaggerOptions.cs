using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Huntask.Common.Presentation.Swagger;

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
    var documentName = Assembly.GetExecutingAssembly().GetName().Name;

    foreach (var description in provider.ApiVersionDescriptions)
    {
      options.SwaggerDoc(description.GroupName, new OpenApiInfo
      {
        Title = $"{documentName} Api {description.GroupName}",
        Version = description.ApiVersion.ToString(),
      });
    }
  }
}
