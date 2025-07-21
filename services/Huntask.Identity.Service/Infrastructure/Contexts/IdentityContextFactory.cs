using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Huntask.Identity.Service.Infrastructure.Contexts;

public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
  public IdentityContext CreateDbContext(string[] args)
  {
    var config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: true)
      .AddEnvironmentVariables()
      .Build();

    var connectionString = config.GetConnectionString("HuntaskDbConnection");

    var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
    optionsBuilder.UseNpgsql(connectionString);

    return new IdentityContext(optionsBuilder.Options);
  }
}