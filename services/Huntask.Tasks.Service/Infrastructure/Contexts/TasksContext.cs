using Microsoft.EntityFrameworkCore;
using Huntask.Identity.Service.Domain.Entities;

namespace Huntask.Identity.Service.Infrastructure.Contexts;

public class TasksContext : DbContext
{
  public TasksContext(DbContextOptions<TasksContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema("tasks");

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