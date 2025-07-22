using Microsoft.EntityFrameworkCore;
using Huntask.Assets.Service.Infrastructure.Contexts;
using Huntask.Assets.Service.Domain.Services;
using Huntask.Assets.Service.Infrastructure.Services;
using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Infrastructure.Repositories;

namespace Huntask.Assets.Service.Infrastructure.Extensions;

public static class ServiceCollectionInfrastructureExtensions
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    services
      .AddDatabase(configuration)
      .AddServices();

    return services;
  }

  private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("HuntaskDbConnection");

    services.AddDbContext<AssetsContext>(opt => opt.UseNpgsql(connectionString));
    services.AddDatabaseDeveloperPageExceptionFilter();

    return services;
  }

  private static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddTransient<IAssetService, FileSystemAssetService>();
    services.AddTransient<IAssetRepository, AssetRepository>();

    return services;
  }
}