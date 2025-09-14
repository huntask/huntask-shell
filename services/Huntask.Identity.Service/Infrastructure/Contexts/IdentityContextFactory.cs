using Huntask.Common.Infrastructure.Models.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Huntask.Identity.Service.Infrastructure.Contexts;

public class IdentityContextFactory() : IDesignTimeDbContextFactory<IdentityContext>
{
  public IdentityContext CreateDbContext(string[] args)
  {
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    var config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: true)
      .AddJsonFile($"appsettings.{env}.json", optional: true)
      .AddEnvironmentVariables()
      .Build();

    var connectionStringOptions = new ConnectionStringsOptions();
    config.GetSection("ConnectionStrings").Bind(connectionStringOptions);

    var connectionString = connectionStringOptions.HuntaskDbConnection ?? config["ConnectionStrings:HuntaskDbConnection"];

    var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
    optionsBuilder.UseNpgsql(connectionString, o => o.MigrationsHistoryTable("__EFMigrationsHistory", "identity"));

    return new IdentityContext(optionsBuilder.Options);
  }
}