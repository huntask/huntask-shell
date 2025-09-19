using Huntask.Assets.Service.Infrastructure.Contexts;

namespace Huntask.Assets.Service.Presentation.Extensions;

public static class ServiceCollectionHealthchecksExtensions
{
  public static IServiceCollection AddHealthCheckServices(this IServiceCollection services)
  {
    services
      .AddHealthChecks()
      .AddDbContextCheck<AssetsContext>(
        name: "huntask-db",
        tags: ["db", "ef"]
      );

    return services;
  }
}