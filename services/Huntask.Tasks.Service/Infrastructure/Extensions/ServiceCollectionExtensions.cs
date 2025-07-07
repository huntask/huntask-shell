using Microsoft.EntityFrameworkCore;
using Huntask.Identity.Service.Domain.Repositories;
using Huntask.Identity.Service.Infrastructure.Contexts;
using Huntask.Identity.Service.Infrastructure.Repositories;

namespace Huntask.Identity.Service.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
  public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
  {
    services
      .AddDatabase(configuration)
      .AddServices();
  }

  private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("HuntaskDbConnection");

    services.AddDbContext<TasksContext>(opt => opt.UseNpgsql(connectionString));
    services.AddDatabaseDeveloperPageExceptionFilter();

    return services;
  }

  private static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddTransient<ITaskItemsRepository, TaskItemsRepository>();
    return services;
  }
}