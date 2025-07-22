using Microsoft.Extensions.DependencyInjection;

namespace Huntask.Common.Presentation.Extensions;

public static class ServiceCollectionPresentationExtensions
{
  public static IServiceCollection AddCommonPresentationServices(this IServiceCollection services)
  {
    services
      .AddApiControllers()
      .AddApiVersioning();

    return services;
  }

  private static IServiceCollection AddApiControllers(this IServiceCollection services)
  {
    services
      .AddControllers()
      .AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
      });

    return services;
  }

  private static IServiceCollection AddApiVersioning(this IServiceCollection services)
  {
    services.AddApiVersioning(options =>
    {
      options.ReportApiVersions = true;
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.DefaultApiVersion = new ApiVersion(1, 0);
    });

    services.AddVersionedApiExplorer(options =>
    {
      options.GroupNameFormat = "'v'VVV";
      options.SubstituteApiVersionInUrl = true;
    });

    return services;
  }
}