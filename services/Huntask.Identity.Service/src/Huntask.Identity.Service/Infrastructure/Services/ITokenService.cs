using Microsoft.AspNetCore.Identity;

namespace Huntask.Identity.Service.Infrastructure.Services;

public interface ITokenService
{
  public string GenerateToken(IdentityUser user);
}