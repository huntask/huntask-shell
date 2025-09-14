using Microsoft.EntityFrameworkCore;
using Huntask.Identity.Service.Domain.Entities;
using Huntask.Tasks.Service.Infrastructure;

namespace Huntask.Identity.Service.Infrastructure.Contexts;

public class TasksContext : DbContext
{
  public TasksContext(DbContextOptions<TasksContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema(Constants.DbSchemaName);

    builder
      .Entity<User>()
      .HasNoKey()
      .ToView(null);

    builder
      .Entity<TaskItem>(entity =>
      {
        entity.Ignore(p => p.IsNull);
        entity.HasKey(p => p.Id);

        entity.Property(p => p.Name).IsRequired();
        entity.Property(p => p.UserId).IsRequired();
      });

    base.OnModelCreating(builder);
  }

  public DbSet<TaskItem> Todos => Set<TaskItem>();
}