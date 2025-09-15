using Microsoft.EntityFrameworkCore;
using Huntask.Assets.Service.Infrastructure.Contexts;
using Huntask.Assets.Service.Domain.Services;
using Huntask.Assets.Service.Infrastructure.Services;
using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Huntask.Common.Infrastructure.Models.Options;
using Huntask.Common.Infrastructure.Extensions;
using Huntask.Assets.Service.Infrastructure.Consumers;

namespace Huntask.Assets.Service.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionInfrastructureExtensions
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
  {
    services
      .AddDatabase()
      .AddMessageBroker<AssetsContext>(opts =>
      {
        opts.AddConsumer<UploadAvatarCommandConsumer>();
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

    services.AddDbContext<AssetsContext>(opt =>
    {
      opt.UseNpgsql(dbConnectionString, npg =>
      {
        npg.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
        npg.MigrationsAssembly(typeof(AssetsContext).Assembly.FullName);
        npg.MigrationsHistoryTable("__EFMigrationsHistory", Constants.DbSchemaName);
      });
    });

    return services;
  }

  private static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddTransient<IAssetService, FileSystemAssetService>();
    services.AddTransient<IAssetRepository, AssetRepository>();

    return services;
  }
}