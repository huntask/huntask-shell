using Huntask.Identity.Service.Infrastructure.Contexts;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class ServiceCollectionHealthchecksExtensions
{
  public static IServiceCollection AddHealthCheckServices(this IServiceCollection services)
  {
    services
      .AddHealthChecks()
      .AddDbContextCheck<IdentityContext>(
        name: "huntask-db",
        tags: ["db", "ef"]
      );

    return services;
  }
}