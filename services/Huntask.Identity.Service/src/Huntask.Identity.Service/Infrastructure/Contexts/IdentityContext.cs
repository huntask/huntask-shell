using Huntask.Identity.Service.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Huntask.Identity.Service.Infrastructure.Contexts;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<User>(options)
{
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema(Constants.DbSchemaName);
    builder.ConfigureMSConsumerOutbox(Constants.DbSchemaName);

    base.OnModelCreating(builder);
  }
}