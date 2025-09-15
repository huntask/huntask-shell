using Microsoft.EntityFrameworkCore;
using Huntask.Identity.Service.Infrastructure.Services;
using Huntask.Identity.Service.Infrastructure.Contexts;
using Huntask.Common.Infrastructure.Models.Options;
using Microsoft.Extensions.Options;
using Huntask.Common.Infrastructure.Extensions;
using Huntask.Identity.Service.Infrastructure.Consumers;

namespace Huntask.Identity.Service.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionInfrastructureExtensions
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
  {
    services
      .AddDatabase()
      .AddMessageBroker<IdentityContext>(opts =>
      {
        opts.AddConsumer<AvatarUploadedEventConsumer>();
      })
      .AddServices();

    return services;
  }

  private static IServiceCollection AddDatabase(this IServiceCollection services)
  {
    var dbConnectionString = services
      .BuildServiceProvider()
      .GetRequiredService<IOptionsSnapshot<ConnectionStringsOptions>>()
      .Value
      .HuntaskDbConnection;

    services.AddDbContext<IdentityContext>(opt =>
    {
      opt.UseNpgsql(dbConnectionString, npg =>
      {
        npg.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
        npg.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName);
        npg.MigrationsHistoryTable("__EFMigrationsHistory", Constants.DbSchemaName);
      });
    });

    return services;
  }

  private static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddTransient<ITokenService, TokenService>();
    return services;
  }
}