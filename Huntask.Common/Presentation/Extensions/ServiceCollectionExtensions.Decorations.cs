using CorrelationId.DependencyInjection;
using Huntask.Common.Presentation.Swagger;
using Microsoft.Extensions.DependencyInjection;

namespace Huntask.Common.Presentation.Extensions;

public static class ServiceCollectionDecorationsExtensions
{
  public static IServiceCollection AddCommonDecorations(this IServiceCollection services)
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
    services.ConfigureOptions<ConfigureSwaggerOptions>();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    return services;
  }
}