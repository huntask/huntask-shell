
using Microsoft.AspNetCore.Identity;

namespace Huntask.Identity.Service.Domain.Models;

public class User : IdentityUser
{
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public string? AvatarAssetId { get; set; }
}
