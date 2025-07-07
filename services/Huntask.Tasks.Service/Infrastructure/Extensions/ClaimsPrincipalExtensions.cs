using System.Security.Claims;
using Huntask.Identity.Service.Domain.Entities;

namespace Huntask.Identity.Service.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
  public static User ToUserEntity(this ClaimsPrincipal user)
  {
    return new User
    {
      Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "",
      UserName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? ""
    };
  }
}
