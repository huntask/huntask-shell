namespace Huntask.Identity.Service.Presentation.Models;

public class LoginResponseApiModel(string? userId)
{
  public string? UserId { get; set; } = userId;
}
