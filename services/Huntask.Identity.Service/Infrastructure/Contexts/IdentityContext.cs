using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Huntask.Identity.Service.Infrastructure.Contexts;

public class IdentityContext : IdentityDbContext<IdentityUser>
{
  public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema("identity");
    base.OnModelCreating(builder);
  }
}