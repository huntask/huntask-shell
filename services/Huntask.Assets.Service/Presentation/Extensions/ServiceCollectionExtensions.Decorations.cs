using CorrelationId.DependencyInjection;
using Huntask.Assets.Service.Presentation.Swagger;
using Microsoft.OpenApi.Models;

namespace Huntask.Assets.Service.Presentation.Extensions;

public static class ServiceCollectionDecorationsExtensions
{
  public static IServiceCollection AddDecorations(this IServiceCollection services)
  {
    services
      .AddCorrelationId()
      .AddCache()
      .AddSwagger();

    return services;
  }

  private static IServiceCollection AddCorrelationId(this IServiceCollection services)
  {
    services.AddDefaultCorrelationId(options =>
    {
      options.IncludeInResponse = true;
      options.RequestHeader = Constants.CorrelationIdHeaderName;
      options.UpdateTraceIdentifier = true;
    });

    return services;
  }

  private static IServiceCollection AddCache(this IServiceCollection services)
  {
    services.AddOutputCache();
    services.AddResponseCaching();

    return services;
  }

  private static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.ConfigureOptions<ConfigureSwaggerOptions>();

    return services;
  }
}