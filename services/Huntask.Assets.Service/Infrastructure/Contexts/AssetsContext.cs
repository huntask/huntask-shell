using Huntask.Assets.Service.Domain.Models;
using Huntask.Common.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Huntask.Assets.Service.Infrastructure.Contexts;

public class AssetsContext(DbContextOptions<AssetsContext> options) : DbContext(options)
{
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema(Constants.DbSchemaName);
    builder.Entity<Asset>(entity =>
    {
      entity.HasKey(a => a.Id);
      entity.Property(a => a.Id).ValueGeneratedNever();
    });

    builder.ConfigureMSConsumerOutbox(Constants.DbSchemaName);

    base.OnModelCreating(builder);
  }

  public DbSet<Asset> Assets => Set<Asset>();
}